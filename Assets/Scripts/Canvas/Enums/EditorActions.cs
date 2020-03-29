namespace VFG.Canvas
{
    public enum LvlEditorAction
    {
        Null                = 0,
        Select              = 500,
        Save                = 1000,
        YESLevelModified    = 1050,
        Replace             = 1100,
        Load                = 2000,
        Import              = 3000,
        Cancel              = 4000,
        CloseReplace        = 4100,
        CloseFileNoExist    = 4150,
        CancelevelModified  = 4200,
        NOLevelModified     = 4300,
        ShowKeyboard        = 4500,
        CloseKeyboard       = 4600,
        Intro               = 5000,
        ArrowUp             = 5100,
        ArrowDown           = 5200,
        Delete              = 6000,
        DeleteYES           = 6100,
        DeleteNO            = 6200,
        CantDelete          = 6300,
        Rename              = 7000,
        RenameClose         = 7100
    }
}