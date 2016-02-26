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

$(document).ready(function () {
    $('.tooltip-with-text').tooltipster();
});

function confirmButtonClick(id)
{
    $.ajax({
        url: '/Admin/ConfirmReview',
        type: 'POST',
        data: "id=" + id,
        success: function (result) {
            $("#" + id).find(".opinion-unchecked").hide(700);
            $("#" + id).find(".opinion-confirm").show(700);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Не вдалося перевірити відгук, помилка: " + errorThrown);
        }
    });
}

function abortButtonClick(id) {
    $.ajax({
        url: '/Admin/AbortReview',
        type: 'DELETE',
        data: "id=" + id,
        success: function (result) {
            $("#" + id).find(".opinion-unchecked").hide(700);
            $("#" + id).find(".opinion-abort").show(700);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Не вдалося перевірити відгук, помилка: " + errorThrown);
        }
    });
}

function delete_image() {        
    $.ajax({
        url: '/Manage/DeleteImage',
        type: 'POST',
        success: function (result) {
            $(".avatar").hide(600);
            $("#delete-img-btn").hide(600);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Не вдалося видалити аватар, помилка: " + errorThrown);
        }
    });
}

function roleChanged(sel, id) {
    var role;
    if (sel.value == 1)
    {
        role = 'Admin';
    }
    if (sel.value == 2)
    {
        role = 'User';
    }
    if (sel.value == 3)
    {
        role = 'Critic';
    }
    $.ajax({
        url: '/Admin/ChangeRole',
        type: 'POST',
        data: "id=" + id + "&role=" + role,
        error: function (xhr, textStatus, errorThrown) {
            alert("Не вдалося змінити роль користувача, помилка: " + errorThrown);
        }
    });
}

function OnWellTopMouseOver(sel)
{
    $(sel).removeClass("well");
    $(sel).addClass("thumbnail")
    
}

function OnWellTopMouseOut(sel) {
    $(sel).removeClass("thumbnail");
    $(sel).addClass("well")

}
