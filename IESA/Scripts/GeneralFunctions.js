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