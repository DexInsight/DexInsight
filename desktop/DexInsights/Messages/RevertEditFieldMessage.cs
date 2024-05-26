using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.Messages {
    public class RevertEditFieldMessage : ValueChangedMessage<int> {
    public RevertEditFieldMessage(int value) : base(value) {
    }
}
}
