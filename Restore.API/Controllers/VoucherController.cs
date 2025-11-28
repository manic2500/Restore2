using Microsoft.AspNetCore.Mvc;
using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;

namespace Restore.API.Controllers;

public class VoucherController(
        ICreateVoucherUseCase create,
        IGetVoucherByIdUseCase getById,
        IListVouchersUseCase list,
        IUpdateVoucherUseCase update,
        IDeleteVoucherUseCase delete) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<List<VoucherDto>>> List()
        => Ok(await list.ExecuteAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VoucherDto>> Get(Guid id)
    {
        var voucher = await getById.ExecuteAsync(id);
        return voucher is null ? NotFound() : Ok(voucher);
    }

    [HttpPost]
    public async Task<ActionResult<VoucherDto>> Create([FromBody] VoucherRequest voucher)
        => Ok(await create.ExecuteAsync(voucher));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VoucherDto>> Update(Guid id, [FromBody] VoucherRequest voucher)
    {
        return Ok(await update.ExecuteAsync(voucher, id));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await delete.ExecuteAsync(id);
        return NoContent();
    }
}
