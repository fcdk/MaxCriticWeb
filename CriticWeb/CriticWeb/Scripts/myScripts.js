$(document).ready(function () {
    $('.tooltip-with-text').tooltipster();

    $('.round-award').each(function (index, value) {
        var tooltipHTMLCode = $(this).next().clone();
        tooltipHTMLCode.show();
        $(this).tooltipster(
        {
            content: tooltipHTMLCode
        });
    });

    $('#stars').rating({
        min: 0, max: 10, step: 1, stars: 10, size: "xs", showClear: false,
        starCaptionClasses: { 1: 'label label-danger', 2: 'label label-danger', 3: 'label label-danger', 4: 'label label-warning', 5: 'label label-warning', 6: 'label label-warning', 7: 'label label-success', 8: 'label label-success', 9: 'label label-success', 10: 'label label-success' }
    });
});

function ok_Click(entertainmentId) {
    $.ajax({
        url: '/Home/OpinionAndLinkCaption',
        type: 'POST',
        data: "opinion=" + $('#opinion').val() + "&link=" + $('#link').val() + "&id=" + entertainmentId,
        success: function (result) {
            $('#review').hide(700);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Не вдалося відправити відгук, помилка: " + errorThrown);
        }
    });
}

function setPoint(entertainmentId)
{
    $('#stars').on('rating.change', function (event, value, caption) {
        $.ajax({
            url: '/Home/RateForContent',
            type: 'POST',
            data: "vote=" + value * 10 + "&id=" + entertainmentId,
            success: function (result) {
                $("#stars").rating("refresh", { readonly: true });
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Не вдалося поставити оцінку контенту, помилка: " + errorThrown);
            }
        });

        $('#review').show(700);
    });
}

function yes_Button_Click(id, helpful, unhelpful)
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

function pageDocReady (paginationId) {
    $('div[id^=page-' + paginationId + ']').hide();
    $('div[id=page-' + paginationId + '-1]').show();
}

function pageWork(paginationId, pageCount)
{
    $('.' + paginationId).bootpag({
        total: pageCount,
        page: 1,
        maxVisible: 10
    }).on('page', function (event, num) {
        $('div[id^=page-' + paginationId + ']').hide();
        $('div[id=page-' + paginationId + '-' + num + ']').show();
    });
}

function setUserRole (usedId, roleValue) {
    $('#' + usedId).find('.form-control').val(roleValue);
}