using EasyLanguageLearning.Domain.ContentSupplying;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.ContentSupplying
{
    public class LearningPathDM : LearningPath
    {
        [Key]
        
        public Guid dbId 
        {
            get => Id.Value;
            set => UpdateId(value);
        }
        public void SetName(string name)
        {
            base.UpdatName(name);
        }

            
    }
}
