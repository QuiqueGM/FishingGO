namespace VFG.Canvas
{
    public enum TypeOfARAction
    {
        None                    = 0,
        BackToMainMenu          = 1,

        MainMenu                = 1000,
        Settings                = 1010,
        Reset                   = 1020,
        NewObjective            = 1100,
        Aquariums               = 1200,
        ShowAquarium            = 1210,
        Objectives              = 1300,
        Play                    = 1310,
        Cards                   = 1400,
        
        BackButtonAquariums     = 2000,
        BackButtonObjectives    = 2001,
        BackButtonCards         = 2003,

        ShowCard                = 3000,
        ShowMainCard            = 3001,
        ShowMoreCard            = 3002,
        ShowClassCard           = 3003,
        ShowQuizCard            = 3004,
        ShowPicture             = 3005,
        SharePicture            = 3006,
		ShowMoreCardFromInGame  = 3007,

        CloseCard               = 3100,
        ClosePicture            = 3101,
        CloseARCanvas           = 3200,

        ObjectiveToFind         = 4000,
        CloseObjectiveToFind    = 4001,
        ReplacePictures         = 5001,

        NewAquarium             = 6100,
        GoToMyNewAquarium       = 6101,
        AllObjectives           = 6200,
        GoToAquariumFinished    = 6201,
        EndGame                 = 6300,

        Continue                = 7000
    }
}