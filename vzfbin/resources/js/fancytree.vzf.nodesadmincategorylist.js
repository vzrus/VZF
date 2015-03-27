function fancyTreeGetCategoriesListJs(selector, treeId, nodeSelectedMsg, jsonData, categories) {
    var categoryList = {};
    categoryList = JSON.parse(categories);
    $('#' + treeId).fancytree(
    { title: 'Fancy Tree',
        toggleEffect: { height: 'toggle', duration: 100 },
        autoFocus: false,
        checkbox: false,
        source: categoryList,   
        beforeExpand: function (event, data) {
            return false;
        },
        activate: function(event, dtnode) {
          $.get(jsonData + '?tjl=-100' + '&active=' + dtnode.node.key,
                function(data) {
                    // alert(nodeSelectedMsg + dtnode.node.title);
                });
        }
    });
}