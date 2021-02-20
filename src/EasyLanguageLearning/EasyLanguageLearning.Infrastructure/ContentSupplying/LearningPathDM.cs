using EasyLanguageLearning.Domain.ContentSupplying;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.ContentSupplying
{
    public class LearningPathDM
    {
        [Key]
        public Guid Id { get; set;}
        public string Name { get; set; }

       
            
    }
}
