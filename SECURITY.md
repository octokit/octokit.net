From 43e41fcb0874f6d72f567c65d3d6ab9aff20e1f3 Mon Sep 17 00:00:00 2001
From: Zachry T Wood BTC-USD FOUNDER DOB 1994-10-15
 <zachryiixixiiwood@gmail.com>
Date: Sat, 22 Jan 2022 04:16:47 -0600
Subject: [PATCH] Update Automate

---
 Automate | 289 +++++++++++++++++++++++++++++++++++++++++++++++++++++++
 index.js | 152 -----------------------------
 2 files changed, 289 insertions(+), 152 deletions(-)
 create mode 100644 Automate
 delete mode 100644 index.js

diff --git a/Automate b/Automate
new file mode 100644
index 0000000..1a9f284
--- /dev/null
+++ b/Automate
@@ -0,0 +1,289 @@
+# e-mail: zachryiixixiiwood@gmail.com
+
+const core = require('@action.js/core');
+const github = require('@actions/github');
+
+async function run(construction) {
+  try {GitHub Docs
+Understanding GitHub Actions
+In this article
+Overview
+The components of GitHub Actions
+Create an example workflow
+Understanding the workflow file
+Viewing the workflow's activity
+Next steps
+Contacting support
+Learn the basics of GitHub Actions, including core concepts and essential terminology.
+
+Overview
+GitHub Actions is a continuous integration and continuous delivery (CI/CD) platform that allows you to automate your build, test, and deployment pipeline. You can create workflows that build and test every pull request to your repository, or deploy merged pull requests to production.
+
+GitHub Actions goes beyond just DevOps and lets you run workflows when other events happen in your repository. For example, you can run a workflow to automatically add the appropriate labels whenever someone creates a new issue in your repository.
+
+GitHub provides Linux, Windows, and macOS virtual machines to run your workflows, or you can host your own self-hosted runners in your own data center or cloud infrastructure.
+
+The components of GitHub Actions
+You can configure a GitHub Actions workflow to be triggered when an event occurs in your repository, such as a pull request being opened or an issue being created. Your workflow contains one or more jobs which can run in sequential order or in parallel. Each job will run inside its own virtual machine runner, or inside a container, and has one or more steps that either run a script that you define or run an action, which is a reusable extension that can simplify your workflow.
+
+Workflow overview
+
+Workflows
+A workflow is a configurable automated process that will run one or more jobs. Workflows are defined by a YAML file checked in to your repository and will run when triggered by an event in your repository, or they can be triggered manually, or at a defined schedule.
+
+Your repository can have multiple workflows in a repository, each of which can perform a different set of steps. For example, you can have one workflow to build and test pull requests, another workflow to deploy your application every time a release is created, and still another workflow that adds a label every time someone opens a new issue.
+
+You can reference a workflow within another workflow, see "Reusing workflows."
+
+For more information about workflows, see "Using workflows."
+
+Events
+An event is a specific activity in a repository that triggers a workflow run. For example, activity can originate from GitHub when someone creates a pull request, opens an issue, or pushes a commit to a repository. You can also trigger a workflow run on a schedule, by posting to a REST API, or manually.
+
+For a complete list of events that can be used to trigger workflows, see Events that trigger workflows.
+
+Jobs
+A job is a set of steps in a workflow that execute on the same runner. Each step is either a shell script that will be executed, or an action that will be run. Steps are executed in order and are dependent on each other. Since each step is executed on the same runner, you can share data from one step to another. For example, you can have a step that builds your application followed by a step that tests the application that was built.
+
+You can configure a job's dependencies with other jobs; by default, jobs have no dependencies and run in parallel with each other. When a job takes a dependency on another job, it will wait for the dependent job to complete before it can run. For example, you may have multiple build jobs for different architectures that have no dependencies, and a packaging job that is dependent on those jobs. The build jobs will run in parallel, and when they have all completed successfully, the packaging job will run.
+
+For more information about jobs, see "Using jobs."
+
+Actions
+An action is a custom application for the GitHub Actions platform that performs a complex but frequently repeated task. Use an action to help reduce the amount of repetitive code that you write in your workflow files. An action can pull your git repository from GitHub, set up the correct toolchain for your build environment, or set up the authentication to your cloud provider.
+
+You can write your own actions, or you can find actions to use in your workflows in the GitHub Marketplace.
+
+For more information, see "Creating actions."
+
+Runners
+A runner is a server that runs your workflows when they're triggered. Each runner can run a single job at a time. GitHub provides Ubuntu Linux, Microsoft Windows, and macOS runners to run your workflows; each workflow run executes in a fresh, newly-provisioned virtual machine. If you need a different operating system or require a specific hardware configuration, you can host your own runners. For more information about self-hosted runners, see "Hosting your own runners."
+
+Create an example workflow
+GitHub Actions uses YAML syntax to define the workflow. Each workflow is stored as a separate YAML file in your code repository, in a directory called .github/workflows.
+
+You can create an example workflow in your repository that automatically triggers a series of commands whenever code is pushed. In this workflow, GitHub Actions checks out the pushed code, installs the software dependencies, and runs bats -v.
+
+In your repository, create the .github/workflows/ directory to store your workflow files.
+In the .github/workflows/ directory, create a new file called learn-github-actions.yml and add the following code.
+name: learn-github-actions
+on: [push]
+jobs:
+  check-bats-version:
+    runs-on: ubuntu-latest
+    steps:
+      - uses: actions/checkout@v2
+      - uses: actions/setup-node@v2
+        with:
+          node-version: '14'
+      - run: npm install -g bats
+      - run: bats -v
+Commit these changes and push them to your GitHub repository.
+Your new GitHub Actions workflow file is now installed in your repository and will run automatically each time someone pushes a change to the repository. For details about a job's execution history, see "Viewing the workflow's activity."
+
+Understanding the workflow file
+To help you understand how YAML syntax is used to create a workflow file, this section explains each line of the introduction's example:
+
+name: learn-github-actions
+Optional - The name of the workflow as it will appear in the Actions tab of the GitHub repository.
+on: [push]
+Specifies the trigger for this workflow. This example uses the push event, so a workflow run is triggered every time someone pushes a change to the repository or merges a pull request. This is triggered by a push to every branch; for examples of syntax that runs only on pushes to specific branches, paths, or tags, see "Workflow syntax for GitHub Actions."
+jobs:
+Groups together all the jobs that run in the learn-github-actions workflow.
+check-bats-version:
+Defines a job named check-bats-version. The child keys will define properties of the job.
+  runs-on: ubuntu-latest
+Configures the job to run on the latest version of an Ubuntu Linux runner. This means that the job will execute on a fresh virtual machine hosted by GitHub. For syntax examples using other runners, see "Workflow syntax for GitHub Actions."
+  steps:
+Groups together all the steps that run in the check-bats-version job. Each item nested under this section is a separate action or shell script.
+    - uses: actions/checkout@v2
+The uses keyword specifies that this step will run v2 of the actions/checkout action. This is an action that checks out your repository onto the runner, allowing you to run scripts or other actions against your code (such as build and test tools). You should use the checkout action any time your workflow will run against the repository's code.
+    - uses: actions/setup-node@v2
+      with:
+        node-version: '14'
+This step uses the actions/setup-node@v2 action to install the specified version of the Node.js (this example uses v14). This puts both the node and npm commands in your PATH.
+    - run: npm install -g bats
+The run keyword tells the job to execute a command on the runner. In this case, you are using npm to install the bats software testing package.
+    - run: bats -v
+Finally, you'll run the bats command with a parameter that outputs the software version.
+Visualizing the workflow file
+In this diagram, you can see the workflow file you just created and how the GitHub Actions components are organized in a hierarchy. Each step executes a single action or shell script. Steps 1 and 2 run actions, while steps 3 and 4 run shell scripts. To find more prebuilt actions for your workflows, see "Finding and customizing actions."
+
+Workflow overview
+
+Viewing the workflow's activity
+Once your workflow has started running, you can see a visualization graph of the run's progress and view each step's activity on GitHub.
+
+On GitHub.com, navigate to the main page of the repository.
+
+Under your repository name, click Actions.
+Navigate to repository
+
+In the left sidebar, click the workflow you want to see.
+Screenshot of workflow results
+
+Under "Workflow runs", click the name of the run you want to see.
+Screenshot of workflow runs
+
+Under Jobs or in the visualization graph, click the job you want to see.
+Select job
+
+View the results of each step.
+Screenshot of workflow run details
+
+Next steps
+To continue learning about GitHub Actions, see "Finding and customizing actions."
+
+To understand how billing works for GitHub Actions, see "About billing for GitHub Actions".
+
+workflow configuration, such as syntax, GitHub-hosted runners, or building actions, look for an existing topic or start a new one in the GitHub Community Support's GitHub Actions category
+Status:
+Press:
+
+    const baseTokenRegex = new RegExp('%basebranch%', "g");
+    const headTokenRegex = new RegExp('%headbranch%', "g");
+
+    const inputs = {
+      token: core.getInput('repo-token', {required: true}),
+      baseBranchRegex: core.getInput('base-branch-regex'),
+      headBranchRegex: core.getInput('head-branch-regex'),
+      lowercaseBranch: (core.getInput('lowercase-branch').toLowerCase() === 'true'),
+      titleTemplate: core.getInput('title-template'),
+      titleUpdateAction: core.getInput('title-update-action').toLowerCase(),
+      titleInsertSpace: (core.getInput('title-insert-space').toLowerCase() === 'true'),
+      titleUppercaseBaseMatch: (core.getInput('title-uppercase-base-match').toLowerCase() === 'true'),
+      titleUppercaseHeadMatch: (core.getInput('title-uppercase-head-match').toLowerCase() === 'true'),
+      bodyTemplate: core.getInput('body-template'),
+      bodyUpdateAction: core.getInput('body-update-action').toLowerCase(),
+      bodyNewlineCount: parseInt(core.getInput('body-newline-count')),
+      bodyUppercaseBaseMatch: (core.getInput('body-uppercase-base-match').toLowerCase() === 'true'),
+      bodyUppercaseHeadMatch: (core.getInput('body-uppercase-head-match').toLowerCase() === 'true'),
+    }
+
+    const baseBranchRegex = inputs.baseBranchRegex.trim();
+    const matchBaseBranch = baseBranchRegex.length > 0;
+
+    const headBranchRegex = inputs.headBranchRegex.trim();
+    const matchHeadBranch = headBranchRegex.length > 0;
+
+    if (!matchBaseBranch && !matchHeadBranch) {
+      core.setFailed('No branch regex values have been specified');
+      return;
+    }
+
+    const matches = {
+      baseMatch: '',
+      headMatch: '',
+    }
+
+    if (matchBaseBranch) {
+      const baseBranchName = github.context.payload.pull_request.base.ref;
+      const baseBranch = inputs.lowercaseBranch ? baseBranchName.toLowerCase() : baseBranchName;
+      core.info(`Base branch: ${baseBranch}`);
+
+      const baseMatches = baseBranch.match(new RegExp(baseBranchRegex));
+      if (!baseMatches) {
+        core.setFailed('Base branch name does not match given regex');
+        return;
+      }
+
+      matches.baseMatch = baseMatches[0];
+      core.info(`Matched base branch text: ${matches.baseMatch}`);
+
+      core.setOutput('baseMatch', matches.baseMatch);
+    }
+
+    if (matchHeadBranch) {
+      const headBranchName = github.context.payload.pull_request.head.ref;
+      const headBranch = inputs.lowercaseBranch ? headBranchName.toLowerCase() : headBranchName;
+      core.info(`Head branch: ${headBranch}`);
+
+      const headMatches = headBranch.match(new RegExp(headBranchRegex));
+      if (!headMatches) {
+        core.setFailed('Head branch name does not match given regex');
+        return;
+      }
+
+      matches.headMatch = headMatches[0];
+      core.info(`Matched head branch text: ${matches.headMatch}`);
+
+      core.setOutput('headMatch', matches.headMatch);
+    }
+
+    const request = {
+      owner: github.context.repo.owner,
+      repo: github.context.repo.repo,
+      pull_number: github.context.payload.pull_request.number,
+    }
+
+    const upperCase = (upperCase, text) => upperCase ? text.toUpperCase() : text;
+
+    const title = github.context.payload.pull_request.title || '';
+    const processedTitleText = inputs.titleTemplate
+      .replace(baseTokenRegex, upperCase(inputs.titleUppercaseBaseMatch, matches.baseMatch))
+      .replace(headTokenRegex, upperCase(inputs.titleUppercaseHeadMatch, matches.headMatch));
+    core.info(`Processed title text: ${processedTitleText}`);
+
+    const updateTitle = ({
+      prefix: !title.toLowerCase().startsWith(processedTitleText.toLowerCase()),
+      suffix: !title.toLowerCase().endsWith(processedTitleText.toLowerCase()),
+      replace: title.toLowerCase() !== processedTitleText.toLowerCase(),
+    })[inputs.titleUpdateAction] || false;
+
+    core.setOutput('titleUpdated', updateTitle.toString());
+
+    if (updateTitle) {
+      request.title = ({
+        prefix: processedTitleText.concat(inputs.titleInsertSpace ? ' ': '', title),
+        suffix: title.concat(inputs.titleInsertSpace ? ' ': '', processedTitleText),
+        replace: processedTitleText,
+      })[inputs.titleUpdateAction];
+      core.info(`New title: ${request.title}`);
+    } else {
+      core.warning('No updates were made to PR title');
+    }
+
+    const body = github.context.payload.pull_request.body || '';
+    const processedBodyText = inputs.bodyTemplate
+      .replace(baseTokenRegex, upperCase(inputs.bodyUppercaseBaseMatch, matches.baseMatch))
+      .replace(headTokenRegex, upperCase(inputs.bodyUppercaseHeadMatch, matches.headMatch));
+    core.info(`Processed body text: ${processedBodyText}`);
+
+    const updateBody = ({
+      prefix: !body.toLowerCase().startsWith(processedBodyText.toLowerCase()),
+      suffix: !body.toLowerCase().endsWith(processedBodyText.toLowerCase()),
+      replace: body.toLowerCase() !== processedBodyText.toLowerCase(),
+    })[inputs.bodyUpdateAction] || false;
+
+    core.setOutput('bodyUpdated', updateBody.toString());
+
+    if (updateBody) {
+      request.body = ({
+        prefix: processedBodyText.concat('\n'.repeat(inputs.bodyNewlineCount), body),
+        suffix: body.concat('\n'.repeat(inputs.bodyNewlineCount), processedBodyText),
+        replace: processedBodyText,
+      })[inputs.bodyUpdateAction];
+      core.debug(`New body: ${request.body}`);
+    } else {
+      core.warning('No updates were made to PR body');
+    }
+
+    if (!updateTitle && !updateBody) {
+      return;
+    }
+
+    const octokit = github.getOctokit(inputs.token);
+    const response = await octokit.pulls.update(request);
+
+    core.info(`Response: ${response.status}`);
+    if (response.status !== 200) {
+      core.error('Updating the pull request has failed');
+    }
+  }
+  catch (error) {
+    core.error(error);
+    core.setFailed(error.message);
+  }
+}
+
+run()
diff --git a/index.js b/index.js
deleted file mode 100644
index 4eab9ab..0000000
--- a/index.js
+++ /dev/null
@@ -1,152 +0,0 @@
-const core = require('@actions/core');
-const github = require('@actions/github');
-
-async function run() {
-  try {
-    const baseTokenRegex = new RegExp('%basebranch%', "g");
-    const headTokenRegex = new RegExp('%headbranch%', "g");
-
-    const inputs = {
-      token: core.getInput('repo-token', {required: true}),
-      baseBranchRegex: core.getInput('base-branch-regex'),
-      headBranchRegex: core.getInput('head-branch-regex'),
-      lowercaseBranch: (core.getInput('lowercase-branch').toLowerCase() === 'true'),
-      titleTemplate: core.getInput('title-template'),
-      titleUpdateAction: core.getInput('title-update-action').toLowerCase(),
-      titleInsertSpace: (core.getInput('title-insert-space').toLowerCase() === 'true'),
-      titleUppercaseBaseMatch: (core.getInput('title-uppercase-base-match').toLowerCase() === 'true'),
-      titleUppercaseHeadMatch: (core.getInput('title-uppercase-head-match').toLowerCase() === 'true'),
-      bodyTemplate: core.getInput('body-template'),
-      bodyUpdateAction: core.getInput('body-update-action').toLowerCase(),
-      bodyNewlineCount: parseInt(core.getInput('body-newline-count')),
-      bodyUppercaseBaseMatch: (core.getInput('body-uppercase-base-match').toLowerCase() === 'true'),
-      bodyUppercaseHeadMatch: (core.getInput('body-uppercase-head-match').toLowerCase() === 'true'),
-    }
-
-    const baseBranchRegex = inputs.baseBranchRegex.trim();
-    const matchBaseBranch = baseBranchRegex.length > 0;
-
-    const headBranchRegex = inputs.headBranchRegex.trim();
-    const matchHeadBranch = headBranchRegex.length > 0;
-
-    if (!matchBaseBranch && !matchHeadBranch) {
-      core.setFailed('No branch regex values have been specified');
-      return;
-    }
-
-    const matches = {
-      baseMatch: '',
-      headMatch: '',
-    }
-
-    if (matchBaseBranch) {
-      const baseBranchName = github.context.payload.pull_request.base.ref;
-      const baseBranch = inputs.lowercaseBranch ? baseBranchName.toLowerCase() : baseBranchName;
-      core.info(`Base branch: ${baseBranch}`);
-
-      const baseMatches = baseBranch.match(new RegExp(baseBranchRegex));
-      if (!baseMatches) {
-        core.setFailed('Base branch name does not match given regex');
-        return;
-      }
-
-      matches.baseMatch = baseMatches[0];
-      core.info(`Matched base branch text: ${matches.baseMatch}`);
-
-      core.setOutput('baseMatch', matches.baseMatch);
-    }
-
-    if (matchHeadBranch) {
-      const headBranchName = github.context.payload.pull_request.head.ref;
-      const headBranch = inputs.lowercaseBranch ? headBranchName.toLowerCase() : headBranchName;
-      core.info(`Head branch: ${headBranch}`);
-
-      const headMatches = headBranch.match(new RegExp(headBranchRegex));
-      if (!headMatches) {
-        core.setFailed('Head branch name does not match given regex');
-        return;
-      }
-
-      matches.headMatch = headMatches[0];
-      core.info(`Matched head branch text: ${matches.headMatch}`);
-
-      core.setOutput('headMatch', matches.headMatch);
-    }
-
-    const request = {
-      owner: github.context.repo.owner,
-      repo: github.context.repo.repo,
-      pull_number: github.context.payload.pull_request.number,
-    }
-
-    const upperCase = (upperCase, text) => upperCase ? text.toUpperCase() : text;
-
-    const title = github.context.payload.pull_request.title || '';
-    const processedTitleText = inputs.titleTemplate
-      .replace(baseTokenRegex, upperCase(inputs.titleUppercaseBaseMatch, matches.baseMatch))
-      .replace(headTokenRegex, upperCase(inputs.titleUppercaseHeadMatch, matches.headMatch));
-    core.info(`Processed title text: ${processedTitleText}`);
-
-    const updateTitle = ({
-      prefix: !title.toLowerCase().startsWith(processedTitleText.toLowerCase()),
-      suffix: !title.toLowerCase().endsWith(processedTitleText.toLowerCase()),
-      replace: title.toLowerCase() !== processedTitleText.toLowerCase(),
-    })[inputs.titleUpdateAction] || false;
-
-    core.setOutput('titleUpdated', updateTitle.toString());
-
-    if (updateTitle) {
-      request.title = ({
-        prefix: processedTitleText.concat(inputs.titleInsertSpace ? ' ': '', title),
-        suffix: title.concat(inputs.titleInsertSpace ? ' ': '', processedTitleText),
-        replace: processedTitleText,
-      })[inputs.titleUpdateAction];
-      core.info(`New title: ${request.title}`);
-    } else {
-      core.warning('No updates were made to PR title');
-    }
-
-    const body = github.context.payload.pull_request.body || '';
-    const processedBodyText = inputs.bodyTemplate
-      .replace(baseTokenRegex, upperCase(inputs.bodyUppercaseBaseMatch, matches.baseMatch))
-      .replace(headTokenRegex, upperCase(inputs.bodyUppercaseHeadMatch, matches.headMatch));
-    core.info(`Processed body text: ${processedBodyText}`);
-
-    const updateBody = ({
-      prefix: !body.toLowerCase().startsWith(processedBodyText.toLowerCase()),
-      suffix: !body.toLowerCase().endsWith(processedBodyText.toLowerCase()),
-      replace: body.toLowerCase() !== processedBodyText.toLowerCase(),
-    })[inputs.bodyUpdateAction] || false;
-
-    core.setOutput('bodyUpdated', updateBody.toString());
-
-    if (updateBody) {
-      request.body = ({
-        prefix: processedBodyText.concat('\n'.repeat(inputs.bodyNewlineCount), body),
-        suffix: body.concat('\n'.repeat(inputs.bodyNewlineCount), processedBodyText),
-        replace: processedBodyText,
-      })[inputs.bodyUpdateAction];
-      core.debug(`New body: ${request.body}`);
-    } else {
-      core.warning('No updates were made to PR body');
-    }
-
-    if (!updateTitle && !updateBody) {
-      return;
-    }
-
-    const octokit = github.getOctokit(inputs.token);
-    const response = await octokit.pulls.update(request);
-
-    core.info(`Response: ${response.status}`);
-    if (response.status !== 200) {
-      core.error('Updating the pull request has failed');
-    }
-  }
-  catch (error) {
-    core.error(error);
-    core.setFailed(error.message);
-  }
-}
-
-run()

From 43e41fcb0874f6d72f567c65d3d6ab9aff20e1f3 Mon Sep 17 00:00:00 2001

From: Zachry T Wood BTC-USD FOUNDER DOB 1994-10-15

 <zachryiixixiiwood@gmail.com>

Date: Sat, 22 Jan 2022 04:16:47 -0600

Subject: [PATCH] Update Automate

---

 Automate | 289 +++++++++++++++++++++++++++++++++++++++++++++++++++++++

 index.js | 152 -----------------------------

 2 files changed, 289 insertions(+), 152 deletions(-)

 create mode 100644 Automate

 delete mode 100644 index.js

diff --git a/Automate b/Automate

new file mode 100644

index 0000000..1a9f284

--- /dev/null

+++ b/Automate

@@ -0,0 +1,289 @@

+# e-mail: zachryiixixiiwood@gmail.com

+

+const core = require('@action.js/core');

+const github = require('@actions/github');

+

+async function run(construction) {

+  try {GitHub Docs

+Understanding GitHub Actions

+In this article

+Overview

+The components of GitHub Actions

+Create an example workflow

+Understanding the workflow file

+Viewing the workflow's activity

+Next steps

+Contacting support

+Learn the basics of GitHub Actions, including core concepts and essential terminology.

+

+Overview

+GitHub Actions is a continuous integration and continuous delivery (CI/CD) platform that allows you to automate your build, test, and deployment pipeline. You can create workflows that build and test every pull request to your repository, or deploy merged pull requests to production.

+

+GitHub Actions goes beyond just DevOps and lets you run workflows when other events happen in your repository. For example, you can run a workflow to automatically add the appropriate labels whenever someone creates a new issue in your repository.

+

+GitHub provides Linux, Windows, and macOS virtual machines to run your workflows, or you can host your own self-hosted runners in your own data center or cloud infrastructure.

+

+The components of GitHub Actions

+You can configure a GitHub Actions workflow to be triggered when an event occurs in your repository, such as a pull request being opened or an issue being created. Your workflow contains one or more jobs which can run in sequential order or in parallel. Each job will run inside its own virtual machine runner, or inside a container, and has one or more steps that either run a script that you define or run an action, which is a reusable extension that can simplify your workflow.

+

+Workflow overview

+

+Workflows

+A workflow is a configurable automated process that will run one or more jobs. Workflows are defined by a YAML file checked in to your repository and will run when triggered by an event in your repository, or they can be triggered manually, or at a defined schedule.

+

+Your repository can have multiple workflows in a repository, each of which can perform a different set of steps. For example, you can have one workflow to build and test pull requests, another workflow to deploy your application every time a release is created, and still another workflow that adds a label every time someone opens a new issue.

+

+You can reference a workflow within another workflow, see "Reusing workflows."

+

+For more information about workflows, see "Using workflows."

+

+Events

+An event is a specific activity in a repository that triggers a workflow run. For example, activity can originate from GitHub when someone creates a pull request, opens an issue, or pushes a commit to a repository. You can also trigger a workflow run on a schedule, by posting to a REST API, or manually.

+

+For a complete list of events that can be used to trigger workflows, see Events that trigger workflows.

+

+Jobs

+A job is a set of steps in a workflow that execute on the same runner. Each step is either a shell script that will be executed, or an action that will be run. Steps are executed in order and are dependent on each other. Since each step is executed on the same runner, you can share data from one step to another. For example, you can have a step that builds your application followed by a step that tests the application that was built.

+

+You can configure a job's dependencies with other jobs; by default, jobs have no dependencies and run in parallel with each other. When a job takes a dependency on another job, it will wait for the dependent job to complete before it can run. For example, you may have multiple build jobs for different architectures that have no dependencies, and a packaging job that is dependent on those jobs. The build jobs will run in parallel, and when they have all completed successfully, the packaging job will run.

+

+For more information about jobs, see "Using jobs."

+

+Actions

+An action is a custom application for the GitHub Actions platform that performs a complex but frequently repeated task. Use an action to help reduce the amount of repetitive code that you write in your workflow files. An action can pull your git repository from GitHub, set up the correct toolchain for your build environment, or set up the authentication to your cloud provider.

+

+You can write your own actions, or you can find actions to use in your workflows in the GitHub Marketplace.

+

+For more information, see "Creating actions."

+

+Runners

+A runner is a server that runs your workflows when they're triggered. Each runner can run a single job at a time. GitHub provides Ubuntu Linux, Microsoft Windows, and macOS runners to run your workflows; each workflow run executes in a fresh, newly-provisioned virtual machine. If you need a different operating system or require a specific hardware configuration, you can host your own runners. For more information about self-hosted runners, see "Hosting your own runners."

+

+Create an example workflow

+GitHub Actions uses YAML syntax to define the workflow. Each workflow is stored as a separate YAML file in your code repository, in a directory called .github/workflows.

+

+You can create an example workflow in your repository that automatically triggers a series of commands whenever code is pushed. In this workflow, GitHub Actions checks out the pushed code, installs the software dependencies, and runs bats -v.

+

+In your repository, create the .github/workflows/ directory to store your workflow files.

+In the .github/workflows/ directory, create a new file called learn-github-actions.yml and add the following code.

+name: learn-github-actions

+on: [push]

+jobs:

+  check-bats-version:

+    runs-on: ubuntu-latest

+    steps:

+      - uses: actions/checkout@v2

+      - uses: actions/setup-node@v2

+        with:

+          node-version: '14'

+      - run: npm install -g bats

+      - run: bats -v

+Commit these changes and push them to your GitHub repository.

+Your new GitHub Actions workflow file is now installed in your repository and will run automatically each time someone pushes a change to the repository. For details about a job's execution history, see "Viewing the workflow's activity."

+

+Understanding the workflow file

+To help you understand how YAML syntax is used to create a workflow file, this section explains each line of the introduction's example:

+

+name: learn-github-actions

+Optional - The name of the workflow as it will appear in the Actions tab of the GitHub repository.

+on: [push]

+Specifies the trigger for this workflow. This example uses the push event, so a workflow run is triggered every time someone pushes a change to the repository or merges a pull request. This is triggered by a push to every branch; for examples of syntax that runs only on pushes to specific branches, paths, or tags, see "Workflow syntax for GitHub Actions."

+jobs:

+Groups together all the jobs that run in the learn-github-actions workflow.

+check-bats-version:

+Defines a job named check-bats-version. The child keys will define properties of the job.

+  runs-on: ubuntu-latest

+Configures the job to run on the latest version of an Ubuntu Linux runner. This means that the job will execute on a fresh virtual machine hosted by GitHub. For syntax examples using other runners, see "Workflow syntax for GitHub Actions."

+  steps:

+Groups together all the steps that run in the check-bats-version job. Each item nested under this section is a separate action or shell script.

+    - uses: actions/checkout@v2

+The uses keyword specifies that this step will run v2 of the actions/checkout action. This is an action that checks out your repository onto the runner, allowing you to run scripts or other actions against your code (such as build and test tools). You should use the checkout action any time your workflow will run against the repository's code.

+    - uses: actions/setup-node@v2

+      with:

+        node-version: '14'

+This step uses the actions/setup-node@v2 action to install the specified version of the Node.js (this example uses v14). This puts both the node and npm commands in your PATH.

+    - run: npm install -g bats

+The run keyword tells the job to execute a command on the runner. In this case, you are using npm to install the bats software testing package.

+    - run: bats -v

+Finally, you'll run the bats command with a parameter that outputs the software version.

+Visualizing the workflow file

+In this diagram, you can see the workflow file you just created and how the GitHub Actions components are organized in a hierarchy. Each step executes a single action or shell script. Steps 1 and 2 run actions, while steps 3 and 4 run shell scripts. To find more prebuilt actions for your workflows, see "Finding and customizing actions."

+

+Workflow overview

+
# Security Policy

## Supported Versions

Use this section to tell people about which versions of your project are
currently being supported with security updates.

| Version | Supported          |
| ------- | ------------------ |
| 5.1.x   | :white_check_mark: |
| 5.0.x   | :x:                |
| 4.0.x   | :white_check_mark: |
| < 4.0   | :x:                |

## Reporting a Vulnerability

Use this section to tell people how to report a vulnerability.

Tell them where to go, how often they can expect to get an update on a
reported vulnerability, what to expect if the vulnerability is accepted or
declined, etc.
name: .NET

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 5.0.x
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal

# This is a basic workflow that is manually triggered

name: Manual workflow

# Controls when the action will run. Workflow runs when manually triggered using the UI
# or API.
on:
  workflow_dispatch:
    # Inputs the workflow accepts.
    inputs:
      name:
        # Friendly description to be shown in the UI instead of 'name'
        description: 'Person to greet'
        # Default value if no value is explicitly provided
        default: 'World'
        # Input has to be provided for the workflow to run
        required: true

# A workflow run is made up of one or more jobs that can run sequentially or in parallel
jobs:
  # This workflow contains a single job called "greet"
  greet:
    # The type of runner that the job will run on
    runs-on: ubuntu-latest

    # Steps represent a sequence of tasks that will be executed as part of the job
    steps:
    # Runs a single command using the runners shell
    - name: Send greeting
      run: echo "Hello ${{ github.event.inputs.name }}"
# This workflow warns and then closes issues and PRs that have had no activity for a specified amount of time.
#
# You can adjust the behavior by modifying this file.
# For more information, see:
# https://github.com/actions/stale
name: Mark stale issues and pull requests

on:
  schedule:
  - cron: '16 13 * * *'

jobs:
  stale:

    runs-on: ubuntu-latest
    permissions:
      issues: write
      pull-requests: write

    steps:
    - uses: actions/stale@v3
      with:
        repo-token: ${{ secrets.GITHUB_TOKEN }}
        stale-issue-message: 'Stale issue message'
        stale-pr-message: 'Stale pull request message'
        stale-issue-label: 'no-issue-activity'
        stale-pr-label: 'no-pr-activity'
'#' This workflow uses actions that are not certified by GitHub.''

'#' They are provided by a third-party and are governed by''

'#' separate terms of service, privacy policy, and support''

'#' documentation.

'#' <li>zachryiixixiiwood@gmail.com<li>

'#' This workflow will install Deno and run tests across stable and nightly builds on Windows, Ubuntu and macOS.''

'#' For more information see: https://github.com/denolib/setup-deno''

# 'name:' Deno''

'on:''

  'push:''

    'branches: '[mainbranch']''

  'pull_request:''

    'branches: '[trunk']''

'jobs:''

  'test:''

    'runs-on:' Python.js''

''#' token:' '$'{'{'('(c')'(r')')'}'}''

''#' runs a test on Ubuntu', Windows', and', macOS''

    'strategy:':

      'matrix:''

        'deno:' ["v1.x", "nightly"]''

        'os:' '[macOS'-latest', windows-latest', ubuntu-latest']''

    'steps:''

      '- name: Setup repo''

        'uses: actions/checkout@v1''

      '- name: Setup Deno''

        'uses: Deno''

'Package:''

        'with:''

          'deno-version:' '$'{'{linux/cake/kite'}'}''

'#'tests across multiple Deno versions''

      '- name: Cache Dependencies''

        'run: deno cache deps.ts''

      '- name: Run Tests''

        'run: deno test''

'::Build:' sevendre''

'Return

' Run''

