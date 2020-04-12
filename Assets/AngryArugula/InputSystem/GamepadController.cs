using Arugula.Peripherals;
public class GamepadController : InputActionsWrapper
{
    public Stick LeftStick = new Stick("1fe017ff-a1cc-4b4d-9010-d6ea970b3cc8");
    public Stick RightStick = new Stick("e15f4c06-e828-4644-871b-5c1ae93ac12c");
    public Button ButtonNorth = new Button("f9680a68-c492-47d5-8c8b-57c6f322cc96");
    public Button ButtonSouth = new Button("7f8f193b-143b-4e8f-805a-93f3350b469e");
    public Button ButtonEast = new Button("08842545-466e-4efb-9977-d32815ad2ebf");
    public Button ButtonWest = new Button("4fed2c69-4e88-4285-b077-09c466dfc29b");
    public Button RightShoulder = new Button("602479b3-460c-4f25-be46-e2ae8bc6f299");
    public Button LeftShoulder = new Button("4b4a7d8a-7425-4329-8545-59edb25408e4");
    public Button RightTrigger = new Button("14ab88bb-a70a-44db-b9a2-686106bdd780");
    public Button LeftTrigger = new Button("617e402f-d3fa-4744-88fb-1c385bb6d5f7");
    public Button Start = new Button("15a1e21a-961c-49e0-aca4-df57593289ea");
    public Button Select = new Button("6fc14049-a660-4387-8a71-263953046e67");
    public Button LeftStickPress = new Button("d65447c2-b69b-4d53-96d3-c450cf68f820");
    public Button RightStickPress = new Button("b1bb63a3-ff6f-4f2f-ba64-708912e83a9b");
    public Stick DPad = new Stick("0458e9de-cc63-4ffb-919f-1b33943e681b");
}