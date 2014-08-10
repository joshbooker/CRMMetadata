using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CRMMetadata.Controllers
{
    public class CreateViewModel
    {
        [Required]
        [RegularExpression("^((?!/).)*$", ErrorMessage = "The tenant name should just be the name of your CRM organization e.g. \"sjkpdev07\" of the following url https://sjkpdev07.crm4.dynamics.com/XRMServices/2011/OrganizationData.svc is the tenant name")]
        public string tenantName { get; set; }


        public Guid id { get; set; }
    }
}
