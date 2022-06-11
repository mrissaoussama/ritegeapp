namespace RitegeDomain.Database.Commands.EventCommands; using RitegeDomain.Database.Entities.ParkingEntities;

public class CreateEventCommand : IRequest<int>
{
    public DateTime DateEvent { get; set; } // dateEvent
    public string HeureEvent { get; set; } // HeureEvent (length: 8)
    public ushort DoorNumber { get; set; } // DoorNumber
    public ushort? UserNumber { get; set; } // userNumber
    public ushort CodeEvent { get; set; } // codeEvent
    public ushort CodeController { get; set; } // codeControler
    public ushort IndiceController { get; set; } // indiceControler
    public bool Selected { get; set; } // selected
    public ushort? Flux { get; set; } // Flux
}