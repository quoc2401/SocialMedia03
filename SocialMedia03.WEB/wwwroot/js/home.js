//const { event } = require("jquery");

const modal = document.querySelector("#modalCreatePost");
const btn_close = document.querySelectorAll(".modal--close-post");
const btn_show = document.querySelectorAll(".btn-show--post");
const loadingTop = $('#loadingTop');
const loadingBottom = $('#loadingBottom');


function showModal() {
    modal.classList.add('open');
}

function closeModal() {
    modal.classList.remove('open');
}

btn_show.forEach(btn => {
    btn.addEventListener("click", showModal);
});

btn_close.forEach(btn => {
    btn.addEventListener("click", closeModal);
});

function loadPosts() {
    $(loadingBottom).css("display", "block");

    $.ajax({
        type: 'get',
        url: `/api/post/feeds?page=${postPage}`,
        dataType: 'json',
        success: function (data) {
            $(loadingBottom).css("display", "none");

            if (data.length === 0) {
                disableLoadMorePost = true;
                return;
            }
            data = data.sort(function (a, b) {
                return new Date(b.createdDate) - new Date(a.createdDate);
            });

            postPage++;
            loadFeeds(data);
        }
    });
}



function homeMenu(menu) {
    let pathName = location.pathname.split("/").find(function (element) {
        if (element.indexOf("/"))
            return element;
    });

    if (pathName !== undefined && pathName !== "follow")
        window.location = `/${menu}`;

    window.scrollTo(0, 0);
    disableLoadMorePost = false;
    $('#feeds-container').empty();
    auctionPage = 1;
    postPage = 1;
    $(loadingBottom).css("display", "block");

    if (menu === '') {
        loadPosts();

        let newPathname = "";
        let newUrl = 'https://' + window.location.host.toString() + newPathname;

        loca = newPathname;
        window.history.replaceState('', 'SocialMedia03', newUrl);
        menuActive(newPathname);
    }
    else if (menu === 'follow') {
        //loadFollowPosts();

        let newPathname = `/follow`;
        let newUrl = 'https://' + window.location.host.toString() + newPathname;

        loca = newPathname;
        window.history.replaceState('', 'SocialMedia03', newUrl);
        menuActive(newPathname);
    }

    event.preventDefault();
}


modal.addEventListener("click", function (event) {
    event.stopPropagation();
});

function menuActive(pathName) {
    $(".menu-active").removeClass("active");
    $("a.nav-link").removeClass("active");

    switch (pathName) {
        case ``:
            $('.homeMenu').addClass('active');
            break;
        case `/admin`:
            $('.chartMenu').addClass('active');
            break;
        case `/follow`:
            $('.followMenu').addClass('active');
            break;
    }
}