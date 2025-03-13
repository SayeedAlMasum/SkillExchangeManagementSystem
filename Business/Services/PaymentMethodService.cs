//PaymentService.cs
using Database.Context;
using Database.Model;

namespace Business.Services
{
    public class PaymentMethodService
    {
        SkillExchangeContext skillExchangeContext = new SkillExchangeContext();
        public Result Add(PaymentMethod paymentMethod)
        {
            if (skillExchangeContext.PaymentMethod.Any(x => x.PaymentMethodName == paymentMethod.PaymentMethodName))
                return new Result(false, "Payment Method already exist!");
            skillExchangeContext.PaymentMethod.Add(paymentMethod);
            return new Result().DBCommit(skillExchangeContext, "Save Successfully!", null, paymentMethod);
        }
        public Result Update(PaymentMethod paymentMethod)
        {
            if (!skillExchangeContext.PaymentMethod.Any(x => x.PaymentMethodId == paymentMethod.PaymentMethodId))
                return new Result(false, "Payment Method not exist!");
            skillExchangeContext.PaymentMethod.Update(paymentMethod);
            return new Result().DBCommit(skillExchangeContext, "Updated Successfully!", null, paymentMethod);
        }
        public Result List()
        {
            try
            {
                var paymentMethods = skillExchangeContext.PaymentMethod.ToList();
                return new Result(true, "Success", paymentMethods);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(int Id)
        {
            try
            {
                var paymentMethod = skillExchangeContext.PaymentMethod.Where(x => x.PaymentMethodId == Id).FirstOrDefault();
                return new Result(true, "Success", paymentMethod);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
