function fancyTreeGetNodesProfileLazyJS(selector,treeId, userId, boardId, echofield, argums, jsonData, initializeArguments, forumUrl) {
    $('#' + treeId).fancytree(
    {
        title: 'Fancy Tree',
        toggleEffect: { height: 'toggle', duration: 200 },
        autoFocus: false,
        checkbox: false,
        source: {
            url: jsonData + 's=' + boardId + initializeArguments + argums + forumUrl
        },
        activate: function(event, data) {
            $('#' + echofield).value = data.node.key;
            if (data.href) {
                window.open(data.node.href, data.node.target);
            }
        },
        lazyLoad: function(event, dtnode) {

            dtnode.result = $.ajax({
                url: jsonData + '=' + dtnode.node.key + argums + forumUrl,
                dataType: 'json'
            });
        }
    });
}

