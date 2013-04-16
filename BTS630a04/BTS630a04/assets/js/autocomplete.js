    $(function () {

        $("#UserName").autocomplete({
            source: "/Link/AutocompleteUsername",
            minLength: 1,
            select: function (event, ui) {
                if (ui.item) {
                    $("#UserName").val(ui.item.value);
                    $("form").submit();
                }
            }
        });
        $("#Role").autocomplete({
            source: "/Link/AutocompleteRole",
            minLength: 1,
            select: function (event, ui) {
                if (ui.item) {
                    $("#Role").val(ui.item.value);
                    $("form").submit();
                }
            }
        });
    });


