function createCommentItem(form, path) {
    var service = new ItemService({ url: '/sitecore/api/ssc/item' });

    var ob = {
        ItemName: 'comment ' + form.name.value,
        TemplateID: '{AB86861A-6030-46C5-B394-E8F99E8B87DB}',
        Name: form.name.value,
        Comment: form.comment.value
    };


    service.create(ob)
        .path(path)
        .execute()
        .then(function (item) {
            form.name.value = '';
            alert('Thanks - comment will appear shortly');
        })
        .fail(function (err) {
            alert(err);
        });

    event.preventDefault();
    return false;
}