using Ardalis.SmartEnum;
using MyVinted.Core.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.SmartEnums
{
    public abstract class LogSortTypeSmartEnum : SmartEnum<LogSortTypeSmartEnum>
    {
        protected LogSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly LogSortTypeSmartEnum DateDescending = new DateDescendingType();
        public static readonly LogSortTypeSmartEnum DateAscending = new DateAscendingType();

        public abstract IEnumerable<LogModel> Sort(IEnumerable<LogModel> logs);

        private sealed class DateDescendingType : LogSortTypeSmartEnum
        {
            public DateDescendingType() : base(nameof(DateDescending), (int)LogSortType.DateDescending) { }

            public override IEnumerable<LogModel> Sort(IEnumerable<LogModel> logs)
                => logs.OrderByDescending(l => l.Date);
        }

        private sealed class DateAscendingType : LogSortTypeSmartEnum
        {
            public DateAscendingType() : base(nameof(DateAscending), (int)LogSortType.DateAscending) { }

            public override IEnumerable<LogModel> Sort(IEnumerable<LogModel> logs)
                => logs.OrderBy(l => l.Date);
        }
    }
}