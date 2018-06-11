using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public class CreditCheckingRepository : RepositoryBase<CcCreditChk>, ICreditCheckingRepository
    {
        private readonly MamApiDb _context;

        public CreditCheckingRepository(MamApiDb context) : base(context)
        {
            _context = context;
        }

        public void SaveConsentReceiveStatus(int creditCheckingId, string userId)
        {
            var creditConsent = _context.CcConsents
                .OrderByDescending(c => c.Id)
                .FirstOrDefault(c => c.CcCreditChkId == creditCheckingId);

            if (creditConsent == null)
            {
                creditConsent = new CcConsent
                {
                    CcCreditChkId = creditCheckingId,
                    ReceiveStatus = BusinessConstant.ConsentStatusOnProcess,
                    ReceivePeriod = 0,
                    Status = BusinessConstant.StatusActive,
                    CreateBy = userId,
                    CreateDate = DateTime.Now
                };

                _context.CcConsents.Add(creditConsent);
            }

            return; //creditConsent.Id;

        }
    }
}
