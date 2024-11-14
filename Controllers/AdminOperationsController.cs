using CologneStore.Constants;
using CologneStore.DTO;
using CologneStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CologneStore.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminOperationsController : Controller
    {
        private readonly IUserOrderRepository _userOrderRepository;

        public AdminOperationsController(IUserOrderRepository userOrderRepository)
        {
            _userOrderRepository = userOrderRepository;
        }

        public async Task<IActionResult> AllOrders()
        {
            var orders = await _userOrderRepository.UserOrders(true);
            return View(orders);
        }

        public async Task<IActionResult> TogglePaymentStatus(int orderId)
        {
            try
            {
                await _userOrderRepository.TogglePaymentStatus(orderId);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(AllOrders));
        }

        public async Task<IActionResult> UpdateOrderStatus(int orderId)
        {
            var order = await _userOrderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id: {orderId} was not found");
            }

            var orderStatusList = (await _userOrderRepository.GetOrderStatus()).Select(orderStatus =>
            {
                return new SelectListItem
                {
                    Value = orderStatus.Id.ToString(),
                    Text = orderStatus.StatusName,
                    Selected = order.OrderStatusId == orderStatus.Id
                };
            });

            var data = new UpdateOrderStatusModel
            {
                OrderId = orderId,
                OrderStatusId = order.OrderStatusId,
                OrderStatusList = orderStatusList
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    data.OrderStatusList = (await _userOrderRepository.GetOrderStatus())
                        .Select(orderStatus =>
                        {
                            return new SelectListItem
                            {
                                Value = orderStatus.Id.ToString(),
                                Text = orderStatus.StatusName,
                                Selected = orderStatus.Id == data.OrderStatusId
                            };
                        });

                    return View(data);
                }

                await _userOrderRepository.ChangeOrderStatus(data);
                
            }
            catch (Exception)
            {

                TempData["errorMessage"] = "Update failed!";
                return View(data);
            }

            TempData["successMessage"] = "Updated Successfully!";
            return RedirectToAction("AllOrders");

            //return RedirectToAction(nameof(UpdateOrderStatus), new { orderId = data.OrderId });
        }
    }
}
