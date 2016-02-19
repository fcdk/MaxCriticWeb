function yes_Button_Click(id,helpful,unhelpful)
{
    $.ajax({
        url: '/Home/RateForReview',
        type: 'POST',
        data: "helpful=" + true + "&id=" + id,
        success: function (result) {
            $("#" + id + "-1").hide(600);
            $("#" + id + "-2").find(".helpful").html(helpful + 1);
            $("#" + id + "-2").find(".unhelpful").html(unhelpful);
            $("#" + id + "-2").show(600);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Не вдалося поставити оцінку відгуку, помилка: " + errorThrown);
        }
    });
}

function no_Button_Click(id, helpful, unhelpful) {
    $.ajax({
        url: '/Home/RateForReview',
        type: 'POST',
        data: "helpful=" + false + "&id=" + id,
        success: function (result) {
            $("#" + id + "-1").hide(600);
            $("#" + id + "-2").find(".helpful").html(helpful);
            $("#" + id + "-2").find(".unhelpful").html(unhelpful + 1);
            $("#" + id + "-2").show(600);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Не вдалося поставити оцінку відгуку, помилка: " + errorThrown);
        }
    });
}