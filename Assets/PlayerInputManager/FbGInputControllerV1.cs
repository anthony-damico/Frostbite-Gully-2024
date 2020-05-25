using Arugula.Peripherals;
public class FbGInputControllerV1 : InputActionsWrapper
{
    public Stick Move = new Stick("f271c7fa-2ebb-4b4f-abbf-fce12ab92e92");
    public Stick Look = new Stick("918c9c5d-7b49-497e-b3f6-9958c7a29a1f");
    public Button Attack = new Button("45ac7300-63de-48c0-8436-c4094c1c0eb1");
    public Stick Navigate = new Stick("d503b565-69fc-43eb-bb6c-4cd964343481");
    public Button Submit = new Button("d83dd00d-12ae-400e-879f-772cc4e3c052");
    public Button Cancel = new Button("aa3da987-4436-4868-b90e-de460b24b729");
    public Button Click = new Button("ba297aa9-d85d-48ef-8b2a-7312765b9099");
    public Button MiddleClick = new Button("cb52900e-fd85-4829-808d-e9f0034d9009");
    public Button RightClick = new Button("76e31b67-0b69-4140-8260-cc1c4ad7efa5");
}