#!/bin/groovy

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

// global variables
repository = "rolfbjarne/TestApp"
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

def reportFinalStatusToSlack (err, gitHash, currentStage, fileContents)
{
    def status = currentBuild.currentResult
    // if ("${status}" == "SUCCESS" && err == "")
    //     return // not reporting success to slack

    def authorName = null
    def authorEmail = null
    if (isPr) {
        authorName = env.CHANGE_AUTHOR_DISPLAY_NAME
        authorEmail = env.CHANGE_AUTHOR_EMAIL
        slackMessage = "Pull Request #<${env.CHANGE_URL}|${env.CHANGE_ID}> failed to build."
    } else {
        authorName = "me"//sh (script: "git log -1 --pretty=%an", returnStdout: true).trim ()
        authorEmail = "me@me.me" //sh (script: "git log -1 --pretty=%ae", returnStdout: true).trim ()
        slackMessage = "Commit <https://github.com/${repository}/commit/${gitHash}|${gitHash}> failed to build."
    }

    def title = null
    if (err != null) {
        title = "Internal jenkins failed in stage '${currentStage}': ${err}"
    } else {
        title = "Internal jenkins failed in stage '${currentStage}'"
    }
    def text = ""
    if (fileContents != null)
        text = "\"text\": ${groovy.json.JsonOutput.toJson (fileContents)},"
    def attachments = """
    [
        {
            \"text\": \"attached\"
        }
    ]
    """
    echo (attachments)
    slackSend (channel: "@rolf", color: "danger", message: "Test message", attachments: attachments.toString())
}


node ('xamarin-macios') {
    stage ("Build") {
        reportFinalStatusToSlack ("some error", "abcdef-hash-abcdef", "Build", "Error details")
    }
} // node
