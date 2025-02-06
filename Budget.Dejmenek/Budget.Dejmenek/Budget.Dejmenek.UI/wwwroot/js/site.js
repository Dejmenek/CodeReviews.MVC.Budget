$(document).ready(function () {
    // Open Create Modal
    $("#btnCreate").click(function () {
        var url = $(this).data("url");

        $.get(url, function (html) {
            $("#modalContent").html(html);
            $("#modal").modal("show");
        });
    });

    // Open Edit Modal
    $(document).on("click", ".btnEdit", function () {
        var url = $(this).data("url");

        $.get(url, function (html) {
            $("#modalContent").html(html);
            $("#modal").modal("show");
        });
    });

    // Handle Save (for Create and Edit)
    $(document).on("click", "#btnSave", function () {
        var form = $("#form");
        var actionUrl = form.attr("action");
        var formData = form.serialize();

        $.post(actionUrl, formData, function (response) {
            if (response.success === true) {
                localStorage.setItem("toastClass", "bg-success");
                localStorage.setItem("toastMessage", response.message);
                location.reload();
            } else if (response.success === false) {
                location.reload();
                localStorage.setItem("toastClass", "bg-danger");
                localStorage.setItem("toastMessage", response.message);
                location.reload();
            } else {
                $("#modalContent").html(response);
            }
        });
    });

    // Handle Delete
    $(document).on("click", "#btnDelete", function () {
        var url = $(this).data("url");
        var id = $(this).data("id");

        $.post(`${url}/${id}`, function (response) {
            var toastClass = response.success === true ? "bg-success" : "bg-danger";
            localStorage.setItem("toastClass", toastClass);
            localStorage.setItem("toastMessage", response.message);
            location.reload();
        });
    });

    // Open Delete Modal
    $(document).on("click", ".btnDelete", function () {
        var id = $(this).data("id");
        var url = $(this).data("url");
        $.get(url, function (response) {
            $("#modalContent").html(response);
            $("#modal").modal("show");
            $("#btnDelete").data("id", id);
        });
    });

    var toastClass = localStorage.getItem("toastClass");
    var toastMessage = localStorage.getItem("toastMessage");

    if (toastClass && toastMessage) {
        $("#toast").addClass(toastClass);
        $("#toastText").text(toastMessage);
        $("#toast").toast("show");

        localStorage.removeItem("toastClass");
        localStorage.removeItem("toastMessage");
    }
});
