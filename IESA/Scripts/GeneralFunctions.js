function getDate() {
    var today = new Date();
    if (today.getDate() > 9 && today.getMonth() + 1 > 9)
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    else if (today.getDate() > 9 && today.getMonth() + 1 < 10)
        var date = today.getFullYear() + '-0' + (today.getMonth() + 1) + '-' + today.getDate();
    else if (today.getDate() < 10 && today.getMonth() + 1 > 9)
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-0' + today.getDate();
    else if (today.getDate() < 10 && today.getMonth() + 1 < 10)
        var date = today.getFullYear() + '-0' + (today.getMonth() + 1) + '-0' + today.getDate();

    return date;
}

function renderDate(date) {

    var day = date.substring(8, 10);
    var month = date.substring(5, 7);
    var hebmonth; // Month in Hebrew
    var year = date.substring(0, 4);

    switch (month) {
        case "01":
            hebmonth = "בינואר";
            break;
        case "02":
            hebmonth = "בפברואר";
            break;
        case "03":
            hebmonth = "במרץ";
            break;
        case "04":
            hebmonth = "באפריל";
            break;
        case "05":
            hebmonth = "במאי";
            break;
        case "06":
            hebmonth = "ביוני";
            break;
        case "07":
            hebmonth = "ביולי";
            break;
        case "08":
            hebmonth = "באוגוסט";
            break;
        case "09":
            hebmonth = "בספטמבר";
            break;
        case "10":
            hebmonth = "באוקטובר";
            break;
        case "11":
            hebmonth = "בנובמבר";
            break;
        case "12":
            hebmonth = "בדצמבר";
            break;

        default:
            hebmonth = "לחודש";
    }

    datestring = day + " " + hebmonth + " " + year;

    return datestring;
}

function renderSixmonths() {

    montharray = [];

    var monthNames = ["ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "אוגוסט", "ספטמבר", "אוקטובר", "נובמבר", "דצמבר"];

    var today = new Date();
    var d;
    var month;

    for (var i = 6; i >= 0; i -= 1) {
        d = new Date(today.getFullYear(), today.getMonth() - i, 1);
        month = monthNames[d.getMonth()];

        montharray.push(month);
    }

    return montharray;
}

function renderNormalDate(date) {

    var day = date.substring(8, 10);
    var month = date.substring(5, 7);
    var year = date.substring(0, 4);

    datestring = day + "/" + month + "/" + year;

    return datestring;
}