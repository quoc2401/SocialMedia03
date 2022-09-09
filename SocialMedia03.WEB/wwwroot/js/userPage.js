
var postPage = 1;
var postFetching = false;
var disableLoadMorePost = false;

function auctionNextPage() {
    if (auctionFetching) return;
    
    auctionPage++;
}

function loadUserPosts(userId) {
    $('.user-loading').css("display", "block");
    auctionFetching = true;

    $.ajax({
        type: 'get',
        url: `/api/post/user/${userId}?page=0`,
        dataType: 'json',
        success: function (data) {
            if (data.length === 0) {
                disableLoadMorePost = true;
                $('.user-loading').css("display", "none");
                $('#feeds-container').append(`<div class="d-flex flex-column justify-content-center align-items-center mt-4">
                                                <img style="width: 100px; height: 100px" src="https://res.cloudinary.com/dynupxxry/image/upload/v1659765073/netflix/star_yepdul.png" />
                                                <p class="text-center">Chưa có bài viết nào</p>
                                            </div>`);
                return;
            }
            
            loadFeeds(data);
            $('.user-loading').css("display", "none");
            postFetching = false;
        }
    });
}

function closeReportUser() {
    $('.modal-report-user').removeClass('open');
}

function openReportUser() {
    $('.modal-report-user').addClass('open');
}

function reportUser(reportedUserId) {
    var reason = $('.modal-report-user').find(':selected').val();
    $.ajax({
            type: 'post',
            url: `/api/user/report-user`,
            data: JSON.stringify({
                'postId': null,
                'userId': reportedUserId,
                'reason': reason,
                'details': ''
            }),
            contentType: 'application/json',
            success: function () {
                swal("Báo cáo người dùng này thành công", {
                    icon: "success"
                });
            }    
        }).fail(function () {
            swal("Có lỗi xảy ra!", {
                icon: "error"
            });
        });
        
        closeReportUser();
}
