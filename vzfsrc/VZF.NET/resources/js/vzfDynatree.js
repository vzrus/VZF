function FancyTreeGetNodesAdminLazyJS(treeId, jsonData, boardId, addarguments, echoActive, forumPath, selector)
{
    $('#' + treeId).fancytree(
            {
                title: 'Lazy Tree',                         
                fx: { height: 'toggle', duration: 200 },
        autoFocus: false,
    initAjax: {
        url: jsonData + '?tjls=' + boardId + forumPath 
    },

    onActivate: function (node) {
        $('#' + echoActive).text(node.data.key + ':' + node.data.title);  
        jQuery(selector).get(jsonData + '?tnm=' + node.data.key);
    },

    onLazyRead: function (node) {
        node.appendAjax({
            url: jsonData + '?tjl=' + node.data.key + addarguments + forumPath});
    }
 
} );

}

