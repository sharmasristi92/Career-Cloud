using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
   public class SystemLanguageCodeLogic: BaseLogic1<SystemLanguageCodePoco>
    {
       
       public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) : base(repository)
        {
        }

        public virtual SystemLanguageCodePoco Get(String ID)
        {
            return _repository.GetSingle(c => c.LanguageID == ID);
        }
        public override void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, "Role cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, "Name cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, "Native name cannot be empty"));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
