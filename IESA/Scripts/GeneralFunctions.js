
function renderNavbar_Site() {

    if (localStorage["user_info"] != null) { //If a user is Logged in

        user_info = JSON.parse(localStorage["user_info"]);
        user_type = JSON.parse(localStorage["user_type"]);

        strnav = "";
        strnav += "<span style='margin-left: 9px;'>שלום " + user_info.Firstname + "</span>";
        strnav += "<span style='margin-left: 9px;'>|</span>";
        strnav += "<a id='disconnectBTN'><i class='bi bi-box-arrow-right' style='position: relative; top: 1px; left: 1.5px;'></i> התנתק </a>";

        document.getElementById("render-nav1").innerHTML = strnav;

        str = "";
        str += "<header class='header'>";
        str += "<div class='top-navbar'>";
        str += "<section class='wrapper'>";
        str += "<div style='display: flex'>";
        str += "<a href='index.html'>";
        str += "<img src='../uploadedFiles/Site-Photos/Iesaprojectlogo.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='margin-right: 1.6rem;'>";

        str += "</a>";
        str += "<span style='margin-right:9px;'></span>";
        str += "<span style='margin-left: 14px; border-left: 1px solid #979797;'></span>";
        str += "<a href='Israel_National_Team.html'>";
        str += "<img src='../uploadedFiles/Site-Photos/navbarisrael.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='height:33px; margin-top: 7px;'>";
        str += "</a>";
        str += "</div>";
        str += "<button type='button' class='opened-menu'>";
        str += "<span></span>";
        str += "<span></span>";
        str += "<span></span>";
        str += "<span></span>";
        str += "</button>";
        str += "<div class='overlay'></div>";
        str += "<nav class='navbar'>";
        str += "<button type='button' class='closed-menu'>";
        str += "<img src='https://www.cssscript.com/demo/responsive-single-level-dropdown-menu/asset/closed.svg' class='closed-icon' alt='closed'>";
        str += "</button>";
        str += "<ul class='menu'>";
        str += "<li class='menu-item'><a href='index.html' id='main-nav'>ראשי</a></li>";
        str += "<li class='menu-item'><a href='competitions.html' id='comp-nav'>תחרויות</a></li>";
        str += "<li class='menu-item'><a href='ranks.html' id='rank-nav'>דירוגים</a></li>";
        str += "<li class='menu-item menu-item-has-children'>";
        str += "<a href='#' data-toggle='sub-menu' id='info-nav'>אודות ומידע<i class='expand'></i></a>";
        str += "<ul class='sub-menu'>";
        str += "<li class='menu-item'><a href='about.html'>אודות העמותה</a></li>";
        str += "<li class='menu-item'><a href='esports.html'>אודות הענף</a></li>";
        str += "<li class='menu-item'><a href='federation.html'>הפדרציה העולמית</a></li>";
        str += "<li class='menu-item'><a href='officials.html'>בעלי תפקידים</a></li>";
        str += "<li class='menu-item'><a href='committee.html'>וועדות</a></li>";
        str += "<li class='menu-item'><a href='regulations.html'>תקנונים</a></li>";
        str += "<li class='menu-item'><a href='volunteer.html'>התנדבות בעמותה</a></li>";
        str += "<li class='menu-item'><a href='new_referee.html'>רישום שופטים</a></li >";
        str += "<li class='menu-item'><a href='post_archive.html'>ארכיון פוסטים</a></li>";
        str += "</ul>";
        str += "</li>";
        str += "<li class='menu-item'><a href='israel_national_team.html' id='isr - nav'>נבחרת ישראל</a></li>";

        if (user_type == "3") { //Gamer
            str += "<li class='menu-item'><a href='gamer_main_page.html' id='pers-nav'>אזור אישי</a></li>";
        }
        else if (user_type == "2") { //Orgenaizer
            str += "<li class='menu-item'><a href='orgenaizer_main_page.html' id='pers-nav'>אזור אישי</a></li>";
        }
        else if (user_type == "1") { //Manager
            str += "<li class='menu-item'><a href='manager_main_page.html' id='pers-nav'>ניהול מערכת</a></li>";
        }

        str += "</ul>";
        str += "</nav>";
        str += "</section>";

        document.getElementById("render-nav2").innerHTML = str;


    }

    else {

        strnav = "";
        strnav += "<span style='margin-left: 9px;'>שלום אורח</span>";
        strnav += "<span style='margin-left: 9px;'>|</span>";
        strnav += "<a id='connectBTN'><i class='bi bi-box-arrow-in-left' style='position: relative; top: 1px; left: 1.5px;'></i> התחבר </a>";

        document.getElementById("render-nav1").innerHTML = strnav;

        str = "";
        str += "<header class='header'>";
        str += "<div class='top-navbar'>";
        str += "<section class='wrapper'>";
        str += "<div style='display: flex'>";
        str += "<a href='index.html'>";
        str += "<img src='../uploadedFiles/Site-Photos/Iesaprojectlogo.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='margin-right: 1.6rem;'>";
        str += "</a>";
        str += "<span style='margin-right:9px;'></span>";
        str += "<span style='margin-left: 14px; border-left: 1px solid #979797;'></span>";
        str += "<a href='Israel_National_Team.html'>";
        str += "<img src='../uploadedFiles/Site-Photos/navbarisrael.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='height:33px; margin-top: 7px;'>";
        str += "</a>";
        str += "</div>";
        str += "<button type='button' class='opened-menu'>";
        str += "<span></span>";
        str += "<span></span>";
        str += "<span></span>";
        str += "<span></span>";
        str += "</button>";
        str += "<div class='overlay'></div>";
        str += "<nav class='navbar'>";
        str += "<button type='button' class='closed-menu'>";
        str += "<img src='https://www.cssscript.com/demo/responsive-single-level-dropdown-menu/asset/closed.svg' class='closed-icon' alt='closed'>";
        str += "</button>";
        str += "<ul class='menu'>";
        str += "<li class='menu-item'><a href='index.html' id='main-nav'>ראשי</a></li>";
        str += "<li class='menu-item'><a href='competitions.html' id='comp-nav'>תחרויות</a></li>";
        str += "<li class='menu-item'><a href='ranks.html' id='rank-nav'>דירוגים</a></li>";
        str += "<li class='menu-item menu-item-has-children'>";
        str += "<a href='#' data-toggle='sub-menu' id='info-nav'>אודות ומידע<i class='expand'></i></a>";
        str += "<ul class='sub-menu'>";
        str += "<li class='menu-item'><a href='about.html'>אודות העמותה</a></li>";
        str += "<li class='menu-item'><a href='esports.html'>אודות הענף</a></li>";
        str += "<li class='menu-item'><a href='federation.html'>הפדרציה העולמית</a></li>";
        str += "<li class='menu-item'><a href='officials.html'>בעלי תפקידים</a></li>";
        str += "<li class='menu-item'><a href='committee.html'>וועדות</a></li>";
        str += "<li class='menu-item'><a href='regulations.html'>תקנונים</a></li>";
        str += "<li class='menu-item'><a href='volunteer.html'>התנדבות בעמותה</a></li>";
        str += "<li class='menu-item'><a href='new_referee.html'>רישום שופטים</a></li >";
        str += "<li class='menu-item'><a href='post_archive.html'>ארכיון פוסטים</a></li>";
        str += "</ul>";
        str += "</li>";
        str += "<li class='menu-item'><a href='israel_national_team.html' id='isr - nav'>נבחרת ישראל</a></li>";

        str += "</ul>";
        str += "</nav>";
        str += "</section>";

        document.getElementById("render-nav2").innerHTML = str;

    }


}

function renderNavbar_Gamer() {

    user_info = JSON.parse(localStorage["user_info"]);

    strnav = "";
    strnav += "<span style='margin-left: 9px;'>שלום " + user_info.Firstname + "</span>";
    strnav += "<span style='margin-left: 9px;'>|</span>";
    strnav += "<a id='disconnectBTN'><i class='bi bi-box-arrow-right' style='position: relative; top: 1px; left: 1.5px;'></i> התנתק </a>";

    document.getElementById("render-nav1").innerHTML = strnav;

    str = "";
    str += "<header class='header'>";
    str += "<div class='top-navbar'>";
    str += "<section class='wrapper'>";
    str += "<div style='display: flex'>";
    str += "<a href='index.html'>";
    str += "<img src='../uploadedFiles/Site-Photos/Iesaprojectlogo.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='margin-right: 1.6rem;'>";
    str += "</a>";
    str += "<span style='margin-right:9px;'></span>";
    str += "<span style='margin-left: 14px; border-left: 1px solid #979797;'></span>";
    str += "<a href='Israel_National_Team.html'>";
    str += "<img src='../uploadedFiles/Site-Photos/navbarisrael.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='height:33px; margin-top: 7px;'>";
    str += "</a>";
    str += "</div>";
    str += "<button type='button' class='opened-menu'>";
    str += "<span></span>";
    str += "<span></span>";
    str += "<span></span>";
    str += "<span></span>";
    str += "</button>";
    str += "<div class='overlay'></div>";
    str += "<nav class='navbar'>";
    str += "<button type='button' class='closed-menu'>";
    str += "<img src='https://www.cssscript.com/demo/responsive-single-level-dropdown-menu/asset/closed.svg' class='closed-icon' alt='closed'>";
    str += "</button>";
    str += "<ul class='menu'>";
    str += "<li class='menu-item'><a href='gamer_main_page.html' id='pers-nav'>אזור אישי</a></li>";
    str += "<li class='menu-item'><a href='gamer_card.html' id='card-nav'>כרטיס שחקן</a></li>";
    str += "<li class='menu-item'><a href='edit_personal_details.html' id='edit-nav'>עריכת פרטים</a></li>";
    str += "<li class='menu-item'><a href='index.html' id='site-nav'>אתר העמותה</a></li>";
    str += "</ul>";
    str += "</nav>";
    str += "</section>";

    document.getElementById("render-nav2").innerHTML = str;

}

function renderNavbar_Orgenaizer() {

    user_info = JSON.parse(localStorage["user_info"]);

    strnav = "";
    strnav += "<span style='margin-left: 9px;'>שלום " + user_info.Firstname + "</span>";
    strnav += "<span style='margin-left: 9px;'>|</span>";
    strnav += "<a id='disconnectBTN'><i class='bi bi-box-arrow-right' style='position: relative; top: 1px; left: 1.5px;'></i> התנתק </a>";

    document.getElementById("render-nav1").innerHTML = strnav;

    str = "";
    str += "<header class='header'>";
    str += "<div class='top-navbar'>";
    str += "<section class='wrapper'>";
    str += "<div style='display: flex'>";
    str += "<a href='index.html'>";
    str += "<img src='../uploadedFiles/Site-Photos/Iesaprojectlogo.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='margin-right: 1.6rem;'>";
    str += "</a>";
    str += "<span style='margin-right:9px;'></span>";
    str += "<span style='margin-left: 14px; border-left: 1px solid #979797;'></span>";
    str += "<a href='Israel_National_Team.html'>";
    str += "<img src='../uploadedFiles/Site-Photos/navbarisrael.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='height:33px; margin-top: 7px;'>";
    str += "</a>";
    str += "</div>";
    str += "<button type='button' class='opened-menu'>";
    str += "<span></span>";
    str += "<span></span>";
    str += "<span></span>";
    str += "<span></span>";
    str += "</button>";
    str += "<div class='overlay'></div>";
    str += "<nav class='navbar'>";
    str += "<button type='button' class='closed-menu'>";
    str += "<img src='https://www.cssscript.com/demo/responsive-single-level-dropdown-menu/asset/closed.svg' class='closed-icon' alt='closed'>";
    str += "</button>";
    str += "<ul class='menu'>";
    str += "<li class='menu-item'><a href='orgenaizer_main_page.html' id='main-nav'>אזור אישי</a></li>";
    str += "<li class='menu-item'><a href='add_new_competition.html' id='comp-nav'>יצירת תחרות</a></li>";
    str += "<li class='menu-item'><a href='org_resource.html' id='rank-nav'>מרכז משאבים</a></li>";
    str += "<li class='menu-item'><a href='edit_personal_details.html' id='edit-nav'>עריכת פרטים</a></li>";
    str += "<li class='menu-item'><a href='index.html' id='site-nav'>אתר העמותה</a></li>";
    str += "</ul>";
    str += "</nav>";
    str += "</section>";

    document.getElementById("render-nav2").innerHTML = str;

}

function renderNavbar_Manager() {

    user_info = JSON.parse(localStorage["user_info"]);

    strnav = "";
    strnav += "<span style='margin-left: 9px;'>שלום " + user_info.Firstname + "</span>";
    strnav += "<span style='margin-left: 9px;'>|</span>";
    strnav += "<a id='disconnectBTN'><i class='bi bi-box-arrow-right' style='position: relative; top: 1px; left: 1.5px;'></i> התנתק </a>";

    document.getElementById("render-nav1").innerHTML = strnav;

    str = "";
    str += "<header class='header'>";
    str += "<div class='top-navbar'>";
    str += "<section class='wrapper'>";
    str += "<div style='display: flex'>";
    str += "<a href='index.html'>";
    str += "<img src='../uploadedFiles/Site-Photos/Iesaprojectlogo.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='margin-right: 1.6rem;'>";
    str += "</a>";
    str += "<span style='margin-right:9px;'></span>";
    str += "<span style='margin-left: 14px; border-left: 1px solid #979797;'></span>";
    str += "<a href='Israel_National_Team.html'>";
    str += "<img src='../uploadedFiles/Site-Photos/navbarisrael.png' alt='העמותה לגיימינג תחרותי בישראל' class='logo-img-primary' style='height:33px; margin-top: 7px;'>";
    str += "</a>";
    str += "</div>";
    str += "<button type='button' class='opened-menu'>";
    str += "<span></span>";
    str += "<span></span>";
    str += "<span></span>";
    str += "<span></span>";
    str += "</button>";
    str += "<div class='overlay'></div>";
    str += "<nav class='navbar'>";
    str += "<button type='button' class='closed-menu'>";
    str += "<img src='https://www.cssscript.com/demo/responsive-single-level-dropdown-menu/asset/closed.svg' class='closed-icon' alt='closed'>";
    str += "</button>";
    str += "<ul class='menu'>";
    str += "<li class='menu-item'><a href='manager_main_page.html' id='mang-nav'>ניהול מערכת</a></li>";
    str += "<li class='menu-item'><a href='database.html' id='data-nav'>מאגר מידע</a></li>";
    str += "<li class='menu-item'><a href='add_new_post.html' id='post-nav'>יצירת פוסט</a></li>";
    str += "<li class='menu-item'><a href='edit_personal_details.html' id='edit-nav'>עריכת פרטים</a></li>";
    str += "<li class='menu-item'><a href='index.html' id='site-nav'>אתר העמותה</a></li>";
    str += "</ul>";
    str += "</nav>";
    str += "</section>";

    document.getElementById("render-nav2").innerHTML = str;

}

function buildNav() {

    //--Navbar Script--
    const openedMenu = document.querySelector('.opened-menu');
    const closedMenu = document.querySelector('.closed-menu');
    const navbarMenu = document.querySelector('.navbar');
    const menuOverlay = document.querySelector('.overlay');

    // Opened navbarMenu
    // Closed navbarMenu
    // Closed navbarMenu by Click Outside
    openedMenu.addEventListener('click', toggleMenu);
    closedMenu.addEventListener('click', toggleMenu);
    menuOverlay.addEventListener('click', toggleMenu);

    // Toggle Menu Function
    function toggleMenu() {
        navbarMenu.classList.toggle('active');
        menuOverlay.classList.toggle('active');
        document.body.classList.toggle('scrolling');
    }

    navbarMenu.addEventListener('click', (event) => {
        if (event.target.hasAttribute('data-toggle') && window.innerWidth <= 992) {
            // Prevent Default Anchor Click Behavior
            event.preventDefault();
            const menuItemHasChildren = event.target.parentElement;

            // If menuItemHasChildren is Expanded, Collapse It
            if (menuItemHasChildren.classList.contains('active')) {
                collapseSubMenu();
            } else {
                // Collapse Existing Expanded menuItemHasChildren
                if (navbarMenu.querySelector('.menu-item-has-children.active')) {
                    collapseSubMenu();
                }
                // Expand New menuItemHasChildren
                menuItemHasChildren.classList.add('active');
                const subMenu = menuItemHasChildren.querySelector('.sub-menu');
                subMenu.style.maxHeight = subMenu.scrollHeight + 'px';
            }
        }
    });

    // Collapse Sub Menu Function
    function collapseSubMenu() {
        navbarMenu.querySelector('.menu-item-has-children.active .sub-menu').removeAttribute('style');
        navbarMenu.querySelector('.menu-item-has-children.active').classList.remove('active');
    }

    // Fixed Resize Screen Function
    function resizeScreen() {
        // If navbarMenu is Open, Close It
        if (navbarMenu.classList.contains('active')) {
            toggleMenu();
        }

        // If menuItemHasChildren is Expanded, Collapse It
        if (navbarMenu.querySelector('.menu-item-has-children.active')) {
            collapseSubMenu();
        }
    }

    window.addEventListener('resize', () => {
        if (this.innerWidth > 992) {
            resizeScreen();
        }
    });
    //--Navbar Script--


}


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
