#!/bin/groovy

node ('xamarin-macios') {
    stage ("A") {
        echo ("Step A")
        manager.buildFailure ()
    }

    stage ("B") {
        echo ("Step B")
        manager.buildFailure ()
    }

    stage ("C") {
        echo ("Step C")
    }
} // node
