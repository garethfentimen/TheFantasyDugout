@ModelType LaGiga.TheListPlayerBasicInfoModel
    
@If Model.PlayerImageLocation <> "NoImage" Then
    @<img src=@Model.PlayerImageLocation alt=""@Model.PlayerName"" />
Else
    @<img src="/Content/no-image-icon-md.png" alt="No Image" title="No Image" />
End If
