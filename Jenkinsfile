#!/bin/groovy

// global variables
repository = "xamarin/xamarin-macios"
isPr = false
branchName = null
gitHash = null
packagePrefix = null
virtualPath = null
xiPackageUrl = null
xmPackageUrl = null
utils = null
errorMessage = null
currentStage = null

xiPackageFilename = null
xmPackageFilename = null
reportPrefix = null

def abortExecutingBuilds ()
{
    // This runs into problems with the Jenkins sandbox:
    //      org.jenkinsci.plugins.scriptsecurity.sandbox.RejectedAccessException: Scripts not permitted to use method jenkins.model.Jenkins getItemByFullName java.lang.String
    // so disable for now.

    def job = Jenkins.instance.getItemByFullName (env.JOB_NAME)
    for (build in job.builds) {
        if (!build.isBuilding ())
            continue
        echo ("build: ${build.getClass ()}")
        if (build.number > currentBuild.number) {
            error ("There is already a newer build in progress (#${build.number})")
        } else if (build.number < currentBuild.number) {
            def exec = build.getExecutor ()
            echo ("build: ${exec.getClass ()}")
            if (exec == null) {
                echo ("No executor for build ${build.number}")
            } else {
                exec.interrupt (Result.ABORTED, new CauseOfInterruption.UserInterruption ("Aborted by build #${currentBuild.number}"))
                echo ("Aborted previous build: #${build.number}")
            }
        }
    }
}

node ('xamarin-macios') {
    // This runs into problems with the Jenkins sandbox:
    manager.createSummary ("warning.gif").appendText ("Hello world!")
    stage ("Checking for previous builds") {
        abortExecutingBuilds ()
    }
    stage ("Build") {
        try {
            sh ("sleep 60")
        } catch (e) {
            sh ("Bad stuff: ${e}")
        }
    }
} // node
