using Microsoft.AspNetCore.Mvc;
using Restore.API.Extensions;
using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Common.DTOs;

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
        var result = await getById.ExecuteAsync(id);
        return this.ToActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<VoucherDto>> Create([FromBody] VoucherRequest voucher)
        => Ok(await create.ExecuteAsync(voucher));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VoucherDto>> Update(Guid id, [FromBody] VoucherRequest voucher)
    {
        var result = await update.ExecuteAsync(voucher, id);
        return this.ToActionResult(result);

    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await delete.ExecuteAsync(id);
        return this.ToActionResult(result);
    }
}
