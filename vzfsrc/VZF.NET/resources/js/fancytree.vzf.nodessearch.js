function fancyTreeGetNodesSearchLazyCheckBoxesJS(selector, treeId, userId, boardId, argums, jsonData, initializeArguments, forumUrl) {
    $('#' + treeId).fancytree(
    {
        title: 'Fancy Tree',
        toggleEffect: { height: 'toggle', duration: 200 },
        checkbox: true,
        selectMode: 1,
        autoFocus: false,
        source: {
            url: jsonData + 's=' + boardId + initializeArguments + forumUrl
        },
        activate: function (event, dtnode) {
            if (dtnode.href) {
                window.open(dtnode.href, dtnode.node.target);
            }
        },
        select: function(event, dtnode) {
            var selKeys = $.map(dtnode.tree.getSelectedNodes(), function(data) {
                return dtnode.node.key;
            });
            var s = selKeys.join('!');
            (selector).get(jsonData + '=-100' + '&selected=' + s, function(data) {
            });
        },
        lazyLoad: function(event, dtnode) {
            dtnode.result = $.ajax({
                url: jsonData + '=' + dtnode.node.key + argums + forumUrl,
                dataType: 'json'
            });
        }
    });
}
