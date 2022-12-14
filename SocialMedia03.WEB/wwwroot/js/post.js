
//Load theo trang cho trang chu
var errorHtml =  `<div class="text-center mt-3 post-loading">
                                <p class="post--content mb-3" style="font-size:30xp;">
                                    Có lỗi xảy ra, không thể đăng bài ngay lúc này!
                                </p>
                                <img class="card-img post--img" src="https://res.cloudinary.com/quoc2401/image/upload/v1659441156/eocshmhivko3pjpa0kkg.png" alt="Error">
                            </div>`;

//Show image after pick picture
function previewImage(el) {
    var oFReader = new FileReader();
    if (el.id === 'uploadImage') {
        oFReader.readAsDataURL(document.querySelector("#uploadImage").files[0]);

        oFReader.onload = function (oFREvent) {
            document.querySelector("#uploadPreview").src = oFREvent.target.result;
        };
        
        $(el).parents('.modal-post').find('.modal--remove-img').css('opacity', '0.6');
    }
    else {
        oFReader.readAsDataURL(document.querySelector("#editImage").files[0]);

        oFReader.onload = function (oFREvent) {
            document.querySelector("#editPreview").src = oFREvent.target.result;
        };
        $(el).parents('.modal-post').find('.modal--remove-img').css('opacity', '0.6');
    }
};

function showFull(element) {
    $("#img01").attr("src", element.src)
    $("#modal01").addClass('open');
}

function createPost() {
    var formData = new FormData();
    var fs = document.getElementById('uploadImage');
    var content = $('#statusContent').val();
    var imgSrc = $('#uploadPreview').prop('src');
    
    if (!isBlank(content) || imgSrc !== undefined)  {
        if(fs.files[0] === undefined) {
            createStatus();
        }
        else {
            var fileType = fs.files[0]['type'];
            var validImageTypes = ['image/jpeg', 'image/png'];
            if (!validImageTypes.includes(fileType)) {
                alert("Không thể nhận loại file này!");
            }
            else {
                $(loadingTop).css('display', 'block');

                for (const file of fs.files) {
                    formData.append("file", file);
                }
                $.ajax({
                    type: 'POST',
                    url: `/api/post/image-upload`,
                    data: formData,
                    dataType: "json",
                    processData: false,
                    cache: false,
                    contentType: false
                })
                .done(function(res){

                    $.ajax({
                        type: 'POST',
                        url: `/api/post/add`,
                        data: JSON.stringify({
                            'content':content,
                            'hashtag': findHashtags(content),
                            'imageUrl':res.data
                        }),
                        dataType : 'json',
                        contentType : 'application/json',
                        success: function (post) {
                            $(loadingTop).css('display', 'none');
                            $('#statusContent').val(null);
                            $('.highlighter').html('');
                            $('uploadImage').val(null);
                            $('#uploadPreview').attr("src", "");
                            prependFeeds(post);
                        }
                    })
                        .fail(function (data) {
                        $(loadingTop).css('display', 'none');
                        $(feedContainer).prepend(errorHtml);
                    });
                })
                    .fail(function (data) {
                    $(loadingTop).css('display', 'none');
                });

                $('.modal-post').removeClass('open');
            }
        }
    }
    else
        alert("Vui lòng nhập nội dung bài viết!");
}

function createStatus() {
    $(loadingTop).css('display', 'block');
    var content = $('#statusContent').val();
    $.ajax({
            type: 'post',
            url: `/api/post/add`,
            data: JSON.stringify({
                'content':content,
                'hashtag': findHashtags(content),
                'imageUrl':''
            }),
            dataType : 'json',
            contentType : 'application/json',
            success: function (data) {
                $(loadingTop).css('display', 'none');
                $('#statusContent').val("");
                $('.highlighter').html('');
                prependFeeds(data);
            }
        })
        .fail(function(){
            $(loadingTop).css('display', 'none');
            $(feedContainer).prepend(errorHtml);
        });
        
        $('.modal-post').removeClass('open');
}

function deletePost(id, el) {
    event.preventDefault();
    swal({
        title: "Bạn có chắc xóa bài viết này?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((isDeleted) => {
        var loadingHtml =   `   <div class="text-center mt-3 post-loading">
                                                <div class="spinner-border text-muted"></div>
                                            </div>
                                        `; 
        var clickedPost = $(el).parents('.post');
        var clickedPostHtml = $(clickedPost).html();

        $(clickedPost).html(loadingHtml);

        $.ajax({
                type: 'delete',
                url: `/api/post/delete/${id}`,
                dataType: 'json',
                success: function () {
                    swal("Xóa bài viết thành công", {
                        icon: "success"
                    });
                    $(clickedPost).remove();
                }
        })
        .fail(function (){
            $(clickedPost).html(clickedPostHtml);
        });
    });
}

function editPost(id, el) {
    event.preventDefault();
    var clickedPost = $(el).parents('.post');
    
    var content = $(clickedPost).find('.post--content').text();
    var imgSrc = $(clickedPost).find('.post--img').prop('src');
    if (imgSrc.indexOf('https://') === -1 || imgSrc.indexOf('localhost') >= 1)
        imgSrc = '';
    modalEditPost(id, content.trim(), imgSrc, "post");
    $("textarea").hashtags();
}

function comfirmEditPost(id) {
    var loadingHtml =   `   <div class="text-center mt-3 post-loading">
                                            <div class="spinner-border text-muted"></div>
                                        </div>
                                                `; 
    var clickedPost = $(`#post-${id}`);
    var clickedPostHtml = $(clickedPost).html();
    
    var content = $('#editingStatusContent').val();
    var imgSrc = $('#editPreview').prop('src');

    if (imgSrc.toLowerCase().indexOf('https://') === -1 || imgSrc.indexOf('localhost') >= 1) {
    
        var formData = new FormData();
        var fs = document.getElementById('editImage');
        var content = $('#editingStatusContent').val();

        if (!isBlank(content) || imgSrc !== undefined)  {
            if(fs.files[0] === undefined) {
                editStatus(id);
            }
            else {
                var fileType = fs.files[0]['type'];
                var validImageTypes = ['image/jpeg', 'image/png'];
                if (!validImageTypes.includes(fileType)) {
                    alert("Không thể nhận loại file này!");
                }
                else {
                    $(clickedPost).html(loadingHtml);
                    removeEditModal();
                    for (const file of fs.files) {
                        formData.append("file", file);
                    }
                    $.ajax({
                        type: 'post',
                        url: `/api/post/image-upload`,
                        data: formData,
                        dataType : "json",
                        processData : false,
                        cache : false,
                        contentType : false
                    })
                    .done(function(res){
                        $.ajax({
                            type: 'PUT',
                            url: `/api/post/edit/${id}`,
                            data: JSON.stringify({
                                'content':content,
                                'hashtag': findHashtags(content),
                                'imageUrl':res.data
                            }),
                            dataType : 'json',
                            contentType : 'application/json',
                            success: function (data2) {
                                $(clickedPost).html(clickedPostHtml);
                                $(clickedPost).find("#timeFromNow").text(moment(data2.createdDate).fromNow());
                                $(clickedPost).find('.post--content').text(data2.content);
                                $(clickedPost).find('.post--img').attr('src', data2.image);
                                $(clickedPost).find('.post--img').css('display', 'block');
                                customHashtag(`#post-${id}`);
                            }
                        })
                        .fail(function(){
                            $(clickedPost).html(clickedPostHtml);
                        });
                    })
                    .fail(function(){
                        $(clickedPost).html(clickedPostHtml);
                    });
                }
            }
        }
        else
            alert("Vui lòng nhập nội dung bài viết!");
    }
    else {
        $(clickedPost).html(loadingHtml);
        removeEditModal();
        $.ajax({
            type: 'put',
            url: `/api/post/edit/${id}`,
            data: JSON.stringify({
                'content':content,
                'hashtag': findHashtags(content),
                'imageUrl':imgSrc.toLowerCase()
            }),
            dataType : 'json',
            contentType : 'application/json',
            success: function (data2) {
                $(clickedPost).html(clickedPostHtml);
                $(clickedPost).find("#timeFromNow").text(moment(data2.createdDate).fromNow());
                $(clickedPost).find('.post--content').text(data2.content);
                $(clickedPost).find('.post--img').attr('src', data2.image);
                customHashtag(`#post-${id}`);
            }
        })
        .fail(function(){
            $(clickedPost).html(clickedPostHtml);
        });
    }
    
}

function editStatus(id) {
    var loadingHtml =   `   <div class="text-center mt-3 post-loading">
                                            <div class="spinner-border text-muted"></div>
                                        </div>
                                                `; 
    var clickedPost = $(`#post-${id}`);
    var clickedPostHtml = $(clickedPost).html();
    var content = $('#editingStatusContent').val();
    
    $(clickedPost).html(loadingHtml);
    removeEditModal();
    $.ajax({
        type: 'put',
        url: `/api/post/edit/${id}`,
        data: JSON.stringify({
            'content':content,
            'hashtag': findHashtags(content),
            'imageUrl':''
        }),
        dataType : 'json',
        contentType : 'application/json',
        success: function (data) {
            $(clickedPost).html(clickedPostHtml);
            $(clickedPost).find("#timeFromNow").text(moment(data.createdDate).fromNow());
            $(clickedPost).find('.post--content').text(data.content);
            $(clickedPost).find('.post--img').css('display', 'none');
            $(clickedPost).find('.post--img').attr('src', '');
            customHashtag(`#post-${id}`);
        }
    })
    .fail(function(){
        $(clickedPost).html(clickedPostHtml);
    });
}