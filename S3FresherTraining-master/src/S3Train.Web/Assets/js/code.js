function LoadImage()
{
    $.ajax("http://localhost:52377/Home/Index")
        .done(function (rs)
    {
        $('#msg').html(rs);
    });
}