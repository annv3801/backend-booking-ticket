using Application.DataTransferObjects.VnPay;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces;
public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}