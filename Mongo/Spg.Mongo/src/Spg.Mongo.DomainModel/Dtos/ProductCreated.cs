using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Mongo.DomainModel.Dtos
{
    public record ProductCreated(int Id, string Name, string Description, DateTime CreationDate);
}
