dotvvm.events.initCompleted.subscribe(function () {

    // restore state
    var lastState = window.sessionStorage.getItem("dotvvmViewHotReloadState");
    window.sessionStorage.removeItem("dotvvmViewHotReloadState");
    if (lastState) {
        dotvvm.serialization.deserialize(JSON.parse(lastState), dotvvm.viewModels.root.viewModel, true);
    }

    // listen for markup file changes
    var hub = $.connection.dotvvmViewHotReloadHub;
    hub.client.fileChanged = function (paths) {

        // serialize viewmodel
        var vm = dotvvm.viewModels.root.viewModel;
        var serialized = dotvvm.serialization.serialize(vm, { serializeAll: true });

        // store it in session storage
        window.sessionStorage.setItem("dotvvmViewHotReloadState", JSON.stringify(serialized));

        // reload
        window.location.reload();
    };
    $.connection.hub.start().
        done(function () { console.log('DotVVM view hot reload active, connection ID=' + $.connection.hub.id); }).
        fail(function () { console.warn('DotVVM view hot reload error!'); });

});