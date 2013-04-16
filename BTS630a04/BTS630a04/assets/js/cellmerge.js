$('tr td.two').each(
    function () {
        var that = $(this),
            next = that.parent().next().find('.two');
        if (next.length) {
            that
                .text(next.remove().text())
                .attr('rowspan', '2');
        }
    });

$('tr td.three').each(
    function () {
        var that = $(this),
            next = that.parent().next().find('.three');
        if (next.length) {
            that
                .text(next.remove().text())
                .attr('rowspan', '2');
        }
    });

$('tr td.one').each(
    function () {
        var that = $(this),
            next = that.parent().next().find('.one');
        if (next.length) {
            that
                .text(next.remove().text())
                .attr('rowspan', '2');
        }
    });