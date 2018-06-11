using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        private readonly MamApiDb _context;

        public AttachmentRepository(MamApiDb context) : base(context)
        {
            _context = context;
        }

    }
}
