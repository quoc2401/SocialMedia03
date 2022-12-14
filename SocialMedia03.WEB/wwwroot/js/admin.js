function closeSideBar() {
    $('.leftside-menu').css("display", "none");
}

function openSideBar() {
    $('.leftside-menu').css("display", "block");
}

function chart(labels, data) {
    const ctx = document.getElementById('myChart').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                    label: 'Thống kê tăng trưởng của SharingHope',
                    data: data,
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function solveReportPost(postId, element) {
    deletePost(postId, element);
    $(element).parents('.report-item').remove();
}

function deleteUser(id) {
    swal({
        title: "Bạn có chắc xóa người dùng này?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((isDeleted) => {
        if (isDeleted)
            $.ajax({
                type: 'delete',
                url: `/api/user/delete/${id}`,
                dataType: 'json',
                success: function () {
                    swal("Xóa người dùng thành công", {
                        icon: "success"
                    });
                    $(`#userItem-${id}`).remove();
                }
            })
                .fail(function () {
                });

    });
}