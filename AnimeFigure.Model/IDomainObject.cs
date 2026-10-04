using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeFigures.Model
{
    /// <summary>
    /// Базовый интерфейс для всех сущностей предметной области
    /// </summary>
    public interface IDomainObject
    {
        int Id { get; set; }
    }
}
