using CologneStore.Data;
using CologneStore.DTO;
using CologneStore.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CologneStore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public CartRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        FirestoreDb firestoreDb = FirestoreDb.Create("cologne-store");

        public async Task<int> AddItem(int cologneId, int quantity)
        {
            string userId = GetUserId();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");
                var cart = await GetCart(userId);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                    };
                    _context.Carts.Add(cart);
                }
                _context.SaveChanges();

                //cart detail section 
                var cartItem = _context.CartDetails.FirstOrDefault(c => c.CartId == cart.Id && c.CologneId == cologneId);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    var cologne = _context.Colognes.Find(cologneId);
                    cartItem = new CartDetail
                    {
                        CologneId = cologneId,
                        CartId = cart.Id,
                        Quantity = quantity,
                        UnitPrice = cologne.Price,
                    };
                    _context.CartDetails.Add(cartItem);
                }
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;

        }
        public async Task<int> RemoveItem(int cologneId)
        {

            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");
                var cart = await GetCart(userId);
                if (cart == null)
                    throw new Exception("Invalid cart");
                //cart detail section 
                var cartItem = _context.CartDetails.FirstOrDefault(c => c.CartId == cart.Id && c.CologneId == cologneId);
                if (cartItem == null)
                {
                    throw new Exception("No items in cart");
                }
                else if (cartItem.Quantity == 1)
                {
                    _context.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;

        }

        public async Task<Cart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userId");
            var cart = await _context.Carts.Include(a => a.CartDetails).ThenInclude(a => a.Cologne)
                .ThenInclude(a => a.Type).Where(a => a.UserId == userId).FirstOrDefaultAsync();

            return cart;
        }

        public async Task<Cart> GetCart(string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }
        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var data = await (from cart in _context.Carts
                              join cartDetail in _context.CartDetails
                              on cart.Id equals cartDetail.CartId
                              select new { cartDetail.Id }).ToListAsync();

            return data.Count;
        }
        public async Task<bool> DoCheckOut(CheckoutModel model)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                //move data from cartdetail to order and order detail then remove cartdetail

                // entery for orderdetail

                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");

                var cart = await GetCart(userId);
                if (cart == null)
                    throw new Exception("Invalid cart");

                var cartDetail = _context.CartDetails.Where(a => a.CartId == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");

                var pendingRecord = _context.OrderStatus.FirstOrDefault
                    (a => a.StatusName == "Pending");
                if (pendingRecord == null)
                    throw new Exception("Order status does not have Pending status");

                var order = new Order
                {
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    UserName = model.Name,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    Address = model.Address,
                    PaymentMethod = model.PaymentMethod,
                    IsPaid = false,
                    OrderStatusId = pendingRecord.Id
                };
                CollectionReference col = firestoreDb.Collection("Orders");
                string id = col.Document().Id;
              
                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        CologneId = item.CologneId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    Dictionary<string, object> data = new Dictionary<string, object>()
                    {
                        {"id",id},
                        {"cologneId", item.CologneId },
                        {"orderId", item.Id },
                        {"quantity", item.Quantity },
                        {"unitPrice", item.UnitPrice },
                    };
                    await col.AddAsync(data);
                    _context.OrderDetails.Add(orderDetail);
                }
                _context.SaveChanges();

                //remove cartdetails
                _context.CartDetails.RemoveRange(cartDetail);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private string GetUserId()
        {
            var principal = _contextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);

            return userId;
        }
    }
}
