using Arugula.Peripherals;
public class FbGInputSystemV2 : InputActionsWrapper
{
    public Stick Move = new Stick("5d33096e-e58c-4391-b93d-fd8a0e1dd842");
    public Stick Look = new Stick("5498f8db-26d6-416f-8351-940ed363e2de");
    public Button Attack = new Button("f09fdf02-d383-4382-9387-48068d415771");
    public Button UIRightClick = new Button("78bda3bf-1cef-4399-b045-14a0169990b9");
    public Button UICancel = new Button("96c1bcd9-2de5-40c1-9c8a-fc001cb6c870");
    public Button UISubmit = new Button("681ee64e-ffe8-4a91-8d93-b84e3873aa65");
    public Stick UINavigate = new Stick("709f9240-2b8d-429e-9810-0394472b25d1");
}