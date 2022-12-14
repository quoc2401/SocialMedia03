var is_show = false;
//var commentPage = 1;
var is_show_formReplyComment = false;
let commentLoading = `<div class="text-center mt-3 comment-loading">
                        <div class="spinner-border text-muted"></div>
                    </div>`;

function addComment(currentPostId, formEl) {
    event.preventDefault();
    var formData = new FormData(formEl);
    const commentContent = formData.get('commentContent');

    if (!isBlank(commentContent)) {
        $(formEl).parents('.comment').find('.comment-loading').css("display", "block");
        $.ajax({
            type: 'post',
            url: `/api/comment/add`,
            data: JSON.stringify({
                'content': commentContent,
                'postId': currentPostId,
                'commentId': null
            }),
            contentType: 'application/json',
            dataType: 'json',
            success: function (data) {
                $(formEl).parents('.comment').find('.comment-loading').css("display", "none");

                var commentSection = $(formEl).parents('.comment').find('#commentedComment');
                $(commentSection).prepend(commentItem(data, currentPostId));

                $(formEl).parents('.comment').find('.add-comment').val("");
            }
        });
    }
}

function createReact(currentPostId, element) {
    var heartReact = $(element).find(".heart-like-button");

    if ($(heartReact).hasClass("liked")) {

        $(heartReact).removeClass("liked");
        likeCounter = parseInt($(element).find('#likeCounter').text()) - 1;
        $(element).find('#likeCounter').text(likeCounter);

        $.ajax({
            type: 'delete',
            url: `/api/react/delete?postId=${currentPostId}`,
            dataType: 'json'
        });
    } else {
        $(heartReact).addClass("liked");
        likeCounter = parseInt($(element).find('#likeCounter').text()) + 1;
        $(element).find('#likeCounter').text(likeCounter);

        $.ajax({
            type: 'post',
            url: `/api/react/add?postId=${currentPostId}`,
            dataType: 'json'
        });
    }
}

function deleteComment(id) {
    var loadingHtml = `   <div class="text-center mt-3 comment-loading">
                            <div class="spinner-border text-muted"></div>
                        </div>
                    `;
    var clickedComment = $(`#commentItem${id}`);
    var clickedCommentHtml = $(clickedComment).html();

    clickedComment.html(loadingHtml);
    swal({
        title: "B???n c?? ch???c x??a b??nh lu???n n??y?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((isDeleted) => {
        $.ajax({
            type: 'delete',
            url: `/api/comment/delete/${id}`,   
            dataType: 'json',
            success: function () {
                swal("X??a b??nh lu???n th??nh c??ng", {
                    icon: "success"
                });
                $(clickedComment).remove();
            }
        })
                .fail(function () {
                    $(clickedComment).html(clickedCommentHtml);
                });
    });
}

function showComment(element, postId) {
    var comment = $(element).parents("div.post").find("div.comment");
    $(comment).find('#commentedComment').empty();

    $(comment).find('.comment-loading').css("display", "block");
    if (comment.hasClass('is-show')) {
        comment.removeClass('is-show');
        $(element).parents('.post').find('#commentPage').val(1);
    } else {
        comment.addClass('is-show');
        loadComment(postId);
    }
    
}

function loadComment(postId) {
    let currentPost = $('#post-' + postId);
    let commentPage = currentPost.find('#commentPage').val();
    var commentedComment = currentPost.find('#commentedComment');

    $.ajax({
            type: 'get',
            url: `/api/comment?page=` + commentPage + '&postId=' + postId,
            dataType: 'json',
            success: function (comments) {
                currentPost.find('.comment-loading').css("display", "none");

                let loadedCommentIds = $('.comment--item').map(function(){
                    return $(this).attr('id');
                }).get();

                comments = comments.sort(function (a, b) {
                    return new Date(b.createdDate) - new Date(a.createdDate);
                });

                let userComment = comments.filter(c => c.user.id === UUID
                    && jQuery.inArray(`commentItem${c.id}`, loadedCommentIds) === -1);
                    
                let othersComment = comments.filter(c => c.user.id !== UUID
                    && jQuery.inArray(`commentItem${c.id}`, loadedCommentIds) === -1);
                
                $(commentedComment).append(`${(userComment).map((comment, index) => {
                    return commentItem(comment, postId);
                }).join('')}`);

                $(commentedComment).append(`${(othersComment).map((comment, index) => {
                    return commentItem(comment, postId);
                }).join('')}`);
                
                commentPage++;
                currentPost.find('#commentPage').val(commentPage);
            }
        })
        .fail(function () {
                    currentPost.find('div.comment').removeClass('comment-is-show');
        })
        .done(function() {
            let count = currentPost.find('#commentedComment').children('.comment--item').length;
            let max = currentPost.find('#commentSetLength').text();
            currentPost.find('#showedCommentLength').text(count);
            if(count == max)
                currentPost.find('.showMore').css('opacity', '0');
            
            let url = new URL(window.location.toString());
            let commentId = url.searchParams.get('comment_id');
            if(commentId === undefined || commentId === null) return;
            if(commentId !== undefined || commentId !== null) {
                if ($('#commentItem' + commentId) !== undefined) {
                    $(window).scrollTop($('#commentItem' + commentId).offset().top - 300);
                    $('#commentItem' + commentId).find('.comment-content' + commentId).addClass('tada');
                }
                else {
                    let commentItem = getComment(commentId);
                }
            }
        });
}

function commentItem(comment, postId) {
    let currentUserAvatar = $("#userAvatar").attr("src");
    let reactSetLength = comment.reacts === null ? 0 : comment.reacts.length;
    let postOwnerId = $(`#post${postId}OwnerId`).val();
    let subLength = comment.commentSetLength;
    
    return `<div id="commentItem${comment.id}" class="d-flex flex-column comment--item py-2 position-relative ${subLength > 0 ? 'child-have-reply' : ''}">
                <div class="point-to-child"></div>
                <div class="d-flex point position-relative">
                    <div class="me-2" style="z-index: 1;">
                    <a href="/user/${comment.user.id}">
                        <img class="comment--avatar rounded-circle" src="${comment.user.avatar}" alt="avatar">
                    </a>
                </div>
                <div class="comment--item-content comment--item-content${comment.id}">
                    <div class="bg-light comment-content comment-content${comment.id}">
                        <div class="d-flex justify-content-start align-items-center">
                            <h6 class="mb-1 me-2 d-flex align-items-center"><a href="/user/${comment.user.id}">${comment.user.lastname} ${comment.user.firstname}
                                ${comment.user.uuid === postOwnerId ?
                            `<span class="author-post"><i class="fa-solid fa-circle-check"></i></span>` : ``}
                            </a></h6>
                            <small class="comment-date">${moment(comment.createdDate).fromNow()}</small>
                        </div>
                        <p class="small mb-0">
                            ${comment.content}
                        </p>

                        <!--count like comment-->
                        <div class="count-like-comment count-like-comment${comment.id} bg-light" 
                            style="display:${reactSetLength > 0 ? `flex` : `none`};">
                            <img class="" height="20" role="presentation" src="data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' viewBox='0 0 16 16'%3e%3cdefs%3e%3clinearGradient id='a' x1='50%25' x2='50%25' y1='0%25' y2='100%25'%3e%3cstop offset='0%25' stop-color='%2318AFFF'/%3e%3cstop offset='100%25' stop-color='%230062DF'/%3e%3c/linearGradient%3e%3cfilter id='c' width='118.8%25' height='118.8%25' x='-9.4%25' y='-9.4%25' filterUnits='objectBoundingBox'%3e%3cfeGaussianBlur in='SourceAlpha' result='shadowBlurInner1' stdDeviation='1'/%3e%3cfeOffset dy='-1' in='shadowBlurInner1' result='shadowOffsetInner1'/%3e%3cfeComposite in='shadowOffsetInner1' in2='SourceAlpha' k2='-1' k3='1' operator='arithmetic' result='shadowInnerInner1'/%3e%3cfeColorMatrix in='shadowInnerInner1' values='0 0 0 0 0 0 0 0 0 0.299356041 0 0 0 0 0.681187726 0 0 0 0.3495684 0'/%3e%3c/filter%3e%3cpath id='b' d='M8 0a8 8 0 00-8 8 8 8 0 1016 0 8 8 0 00-8-8z'/%3e%3c/defs%3e%3cg fill='none'%3e%3cuse fill='url(%23a)' xlink:href='%23b'/%3e%3cuse fill='black' filter='url(%23c)' xlink:href='%23b'/%3e%3cpath fill='white' d='M12.162 7.338c.176.123.338.245.338.674 0 .43-.229.604-.474.725a.73.73 0 01.089.546c-.077.344-.392.611-.672.69.121.194.159.385.015.62-.185.295-.346.407-1.058.407H7.5c-.988 0-1.5-.546-1.5-1V7.665c0-1.23 1.467-2.275 1.467-3.13L7.361 3.47c-.005-.065.008-.224.058-.27.08-.079.301-.2.635-.2.218 0 .363.041.534.123.581.277.732.978.732 1.542 0 .271-.414 1.083-.47 1.364 0 0 .867-.192 1.879-.199 1.061-.006 1.749.19 1.749.842 0 .261-.219.523-.316.666zM3.6 7h.8a.6.6 0 01.6.6v3.8a.6.6 0 01-.6.6h-.8a.6.6 0 01-.6-.6V7.6a.6.6 0 01.6-.6z'/%3e%3c/g%3e%3c/svg%3e" width="20">
                            <span id="count-liked-comment${comment.id}" class="ms-1">${reactSetLength}</span>
                        </div>

                    </div>
                      <div class="d-flex justify-content-end me-2">
                            ${reactSetLength === 0 ? `
                                <div class="comment-like comment-like${comment.id}" onclick="likedComment(${comment.id})">Th??ch</div>` : (
            (comment.reacts).some((react) => react.user.uuid === UUID) ?
            `<div class="comment-like comment-like${comment.id} liked" onclick="likedComment(${comment.id})">???? Th??ch</div>` :
            `<div class="comment-like comment-like${comment.id}" onclick="likedComment(${comment.id})">Th??ch</div>`
            )}


                            <div class="comment-reply" onclick="showFormReply(${comment.id})">Ph???n h???i</div>
                            ${(UUID === comment.user.uuid || UUID === postOwnerId) ?
                        `<div class="comment-edit" onclick="showEditComment(${comment.id}, ${postId})">S???a</div>
                        <div class="comment-delete" onclick="deleteComment(${comment.id})">X??a</div>` : ``}
                      </div>
                  </div>
                </div>
                <!--Dung de show reply-->
                <div id="commentReplies${comment.id}" class="reply-comments position-relative">
                    <div class="align-items-center my-2 commentFormReply position-relative"  id="commentFormReply${comment.id}">
                        <div class="me-2">
                            <a href="#">
                                <img class="comment--avatar rounded-circle" src="${currentUserAvatar}" alt="avatar">
                            </a>
                        </div>
                        <div class="point-to-formReply"></div>
                        <form class="w-100" onsubmit="addReply(${comment.id}, this, ${postId})">
                            <input name="commentContent" type="text" placeholder="Th??m b??nh lu???n" class="add-comment" />
                        </form>
                    </div>

                        <div class="repliedComments" id="repliedComments${comment.id}">

                        </div>

                        <param id="replyPage" value="1"/>
                    </div>
                ${subLength > 0 ? `
                    <div class="btn-load-reply-comments" id="loadReply${comment.id}" onclick="loadReplies(${comment.id}, ${postId})">
                            <div class="point-to-showMore"></div>
                            <i class="fa-solid fa-reply me-2"></i>
                            <span>Xem <span class="count-reply" id="countReply${comment.id}">${subLength}</span> ph???n h???i</span>
                `:``}
                </div>
            </div>`;
}

function likedComment(commentItemId) {
    let cItem = $(`#commentItem${commentItemId}`);
    let likeBtn = cItem.find(`.comment-like${commentItemId}`);

    if (likeBtn.hasClass('liked')) {
        likeBtn.text("Th??ch");
        likeBtn.removeClass('liked');
        likeCounter = parseInt(cItem.find(`#count-liked-comment${commentItemId}`).text()) - 1;
        cItem.find(`#count-liked-comment${commentItemId}`).text(likeCounter);
        if (likeCounter < 1) {
            cItem.find(`.count-like-comment${commentItemId}`).css('display', 'none');
        }

        $.ajax({
            type: 'delete',
            url: `/api/react/delete?commentId=${commentItemId}`,
            dataType: 'json'
        });
    } else {
        likeBtn.text("???? Th??ch");
        likeBtn.addClass('liked');
        likeCounter = parseInt(cItem.find(`#count-liked-comment${commentItemId}`).text()) + 1;
        cItem.find(`#count-liked-comment${commentItemId}`).text(likeCounter);
        if (likeCounter > 0) {
            cItem.find(`.count-like-comment${commentItemId}`).css('display', 'flex');
        }

        $.ajax({
            type: 'post',
            url: `/api/react/add?commentId=${commentItemId}`,
            dataType: 'json'
        });
    }
}

function showFormReply(commentItemId) {
    const formReply = $(`#commentFormReply${commentItemId}`);
    if (formReply.hasClass('form-reply-is-show')) {
        formReply.removeClass('form-reply-is-show');
    } else {
        formReply.addClass('form-reply-is-show');
    }
}

function loadReplies(commentId, postId) {
    let currentComment = $('#commentItem' + commentId);
    var repliedComment = currentComment.find(`#repliedComments${commentId}`);

    $.ajax({
        type: 'get',
        url: `/api/comment/get-replies?page=0&commentId=${commentId}`,
        dataType: 'json',
        success: function (comments) {
            let loadedCommentIds = $('.comment--item').map(function () {
                return $(this).attr('id');
            }).get();

                let userComment = comments.filter(c => c.user.id === UUID
                        && jQuery.inArray(`commentItem${c.id}`, loadedCommentIds) === -1);
                userComment.sort(function (a, b) {
                    return new Date(b.createdDate) - new Date(a.createdDate);
                });
                let othersComment = comments.filter(c => c.user.id !== UUID
                        && jQuery.inArray(`commentItem${c.id}`, loadedCommentIds) === -1);
                othersComment.sort(function (a, b) {
                    return new Date(b.createdDate) - new Date(a.createdDate);
                });

                $(repliedComment).append(`${(userComment).map((comment, index) => {
                    return commentItem(comment, postId);
                }).join('')}`);

                $(repliedComment).append(`${(othersComment).map((comment, index) => {
                    return commentItem(comment, postId);
                }).join('')}`);
            $('#loadReply' + commentId).remove();
        }
    })
            .done(function () {
//            let url = new URL(window.location.toString());
//            let commentId = url.searchParams.get('comment_id');
//            if(commentId === undefined || commentId === null) return;
//            if(commentId !== undefined || commentId !== null) {
//                $(window).scrollTop($('#commentItem' + commentId).offset().top - 300);
//                $('#commentItem' + commentId).find('.comment-content' + commentId).addClass('tada');
//            }
            });
}

function addReply(currentCommentId, formEl, postId) {
    event.preventDefault();
    var formData = new FormData(formEl);
    let commentContent = formData.get('commentContent');
    let currentComment = $(`#commentItem${currentCommentId}`);

    if (!isBlank(commentContent)) {
        currentComment.find(`#repliedComments${currentCommentId}`).prepend(commentLoading);
        $.ajax({
            type: 'post',
            url: `/api/comment/add`,
            data: JSON.stringify({
                'content': commentContent,
                'postId': null,
                'commentId': currentCommentId
            }),
            contentType: 'application/json',
            dataType: 'json',
            success: function (data) {
                currentComment.find('.comment-loading').remove();

                currentComment.find(`#repliedComments${currentCommentId}`).prepend(commentItem(data, postId));

                currentComment.find('input[name=commentContent]').val("");
            }
        });
    }
}

function formEditComment(commentId, postId) {
    return `<div style="width: 188px">
                <form class="w-100" onsubmit="editComment(${commentId}, this, ${postId})">
                    <input name="editContent" style="height: 70px" placeholder="Aa" rows=2 id="form-edit-comment${commentId}" class="form-edit-comment"></input>
                </form>
                <div class="cancel-edit-comment cancel-edit-comment${commentId}">H???y</div>
            </div>`
}

function showEditComment(commentId, postId) {
    const currentComment = $(`.comment--item-content${commentId}`).html();
    $(`.comment--item-content${commentId}`).html(formEditComment(commentId, postId));
    $(`.cancel-edit-comment${commentId}`).on('click', function () {
        $(`.comment--item-content${commentId}`).empty();
        $(`.comment--item-content${commentId}`).append(currentComment);
    })
}

function editComment(commentId, formEl, postId) {
    event.preventDefault();
    var formData = new FormData(formEl);
    const commentContent = formData.get('editContent');
    const currentComment = $(`.comment--item-content${commentId}`);

    if (!isBlank(commentContent)) {
        currentComment.html(commentLoading);
        $.ajax({
            type: 'put',
            url: `/api/comment/edit/${commentId}`,
            data: JSON.stringify({
                'content': commentContent,
                'postId': null,
                'commentId': null
            }),
            contentType: 'application/json',
            dataType: 'json',
            success: function (comment) {
                console.log(comment);
                currentComment.find('.comment-loading').remove();
                let reactSetLength = comment.reacts === null ? 0 : comment.reacts.length;
                let postOwnerId = $(`#post${postId}OwnerId`).val();
                currentComment.html(`<div class="bg-light comment-content comment-content${comment.id}">
                        <div class="d-flex justify-content-start align-items-center">
                            <h6 class="mb-1 me-2 d-flex align-items-center"><a href="/user/${comment.user.uuid}">${comment.user.lastname} ${comment.user.firstname}
                                ${comment.user.uuid === postOwnerId ?
                        `<span class="author-post"><i class="fa-solid fa-circle-check"></i></span>` : ``}
                            </a></h6>
                            <small class="comment-date">${moment(comment.createdDate).fromNow()}</small>
                        </div>
                        <p class="small mb-0">
                            ${comment.content}
                        </p>

                        <!--count like comment-->
                        <div class="count-like-comment count-like-comment${comment.id} bg-light" 
                            style="display:${reactSetLength > 0 ? `flex` : `none`};">
                            <img class="" height="20" role="presentation" src="data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' viewBox='0 0 16 16'%3e%3cdefs%3e%3clinearGradient id='a' x1='50%25' x2='50%25' y1='0%25' y2='100%25'%3e%3cstop offset='0%25' stop-color='%2318AFFF'/%3e%3cstop offset='100%25' stop-color='%230062DF'/%3e%3c/linearGradient%3e%3cfilter id='c' width='118.8%25' height='118.8%25' x='-9.4%25' y='-9.4%25' filterUnits='objectBoundingBox'%3e%3cfeGaussianBlur in='SourceAlpha' result='shadowBlurInner1' stdDeviation='1'/%3e%3cfeOffset dy='-1' in='shadowBlurInner1' result='shadowOffsetInner1'/%3e%3cfeComposite in='shadowOffsetInner1' in2='SourceAlpha' k2='-1' k3='1' operator='arithmetic' result='shadowInnerInner1'/%3e%3cfeColorMatrix in='shadowInnerInner1' values='0 0 0 0 0 0 0 0 0 0.299356041 0 0 0 0 0.681187726 0 0 0 0.3495684 0'/%3e%3c/filter%3e%3cpath id='b' d='M8 0a8 8 0 00-8 8 8 8 0 1016 0 8 8 0 00-8-8z'/%3e%3c/defs%3e%3cg fill='none'%3e%3cuse fill='url(%23a)' xlink:href='%23b'/%3e%3cuse fill='black' filter='url(%23c)' xlink:href='%23b'/%3e%3cpath fill='white' d='M12.162 7.338c.176.123.338.245.338.674 0 .43-.229.604-.474.725a.73.73 0 01.089.546c-.077.344-.392.611-.672.69.121.194.159.385.015.62-.185.295-.346.407-1.058.407H7.5c-.988 0-1.5-.546-1.5-1V7.665c0-1.23 1.467-2.275 1.467-3.13L7.361 3.47c-.005-.065.008-.224.058-.27.08-.079.301-.2.635-.2.218 0 .363.041.534.123.581.277.732.978.732 1.542 0 .271-.414 1.083-.47 1.364 0 0 .867-.192 1.879-.199 1.061-.006 1.749.19 1.749.842 0 .261-.219.523-.316.666zM3.6 7h.8a.6.6 0 01.6.6v3.8a.6.6 0 01-.6.6h-.8a.6.6 0 01-.6-.6V7.6a.6.6 0 01.6-.6z'/%3e%3c/g%3e%3c/svg%3e" width="20">
                            <span id="count-liked-comment${comment.id}" class="ms-1">${reactSetLength}</span>
                        </div>

                    </div>
                      <div class="d-flex justify-content-end me-2">
                            ${reactSetLength === 0 ? `
                                <div class="comment-like comment-like${comment.id}" onclick="likedComment(${comment.id})">Th??ch</div>` : (
                        (comment.reacts).some((react) => react.user.uuid === UUID) ?
                            `<div class="comment-like comment-like${comment.id} liked" onclick="likedComment(${comment.id})">???? Th??ch</div>` :
                            `<div class="comment-like comment-like${comment.id}" onclick="likedComment(${comment.id})">Th??ch</div>`
                    )}


                            <div class="comment-reply" onclick="showFormReply(${comment.id})">Ph???n h???i</div>
                            ${(UUID === comment.user.uuid || UUID === postOwnerId) ?
                        `<div class="comment-edit" onclick="showEditComment(${comment.id})">S???a</div>
                        <div class="comment-delete" onclick="deleteComment(${comment.id})">X??a</div>` : ``}
                      </div>`);

            }
        }).fail(function (res) {
            console.log(res);
        });
    }
}