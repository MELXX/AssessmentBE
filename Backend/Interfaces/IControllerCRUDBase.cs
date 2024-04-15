using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces
{
    public interface IControllerCRUDBase<RequestDTO,ResponseDTO>
    {
        Task<IActionResult> Create(RequestDTO request);
        Task<IActionResult> Read(Guid Id);
        /// <summary>
        /// Returns a list of ResponseDTO 
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> Read();
        Task<IActionResult> Update(RequestDTO request);
        Task<IActionResult> Delete(Guid Id);
    }
}
