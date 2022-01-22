name: CI Build

on:
  pull_request:
    branches:
      - "main"
  push:
    branches:
      - "main"

jobs:
  build:

    strategy:
      matrix:
        platform: [ubuntu-latest, macos-latest, windows-latest]

    runs-on: ${{ matrix.platform }}

    steps:
    - uses: actions/checkout@v1
    - name: Setup .NET 5
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 5.0.*
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 3.1.*
    - name: Build with dotnet
      run: ./build.sh --linksources=true --verbosity=verbose
      shell: bash
diff --git a/.github/workflows/ci.yaml b/.github/workflows/ci.yaml
deleted file mode 100644
index c5d3968b..00000000
--- a/.github/workflows/ci.yaml
+++ /dev/null
@@ -1,33 +0,0 @@
-on:
-  push:
-    branches:
-      - master
-  pull_request:
-    types: [ assigned, opened, synchronize, reopened, labeled ]
-name: ci
-jobs:
-  test:
-    runs-on: ${{ matrix.os }}
-    strategy:
-      matrix:
-        node: [10, 12, 14]
-        os: [ubuntu-latest, windows-latest]
-    env:
-        OS: ${{ matrix.os }}
-        NODE_VERSION: ${{ matrix.node }}
-    steps:
-      - uses: actions/checkout@v2
-      - run: git fetch --prune --unshallow
-      - run: git config --global user.name 'Actions'
-      - run: git config --global user.email 'dummy@example.org'
-      - uses: actions/setup-node@v2
-        with:
-          node-version: ${{ matrix.node }}
-      - run: node --version
-      - run: npm install --engine-strict
-      - run: npm test
-      - run: npm run coverage
-      - name: Codecov
-        uses: codecov/codecov-action@v2
-        with:
-          env_vars: OS, NODE_VERSION
diff --git a/bitore.sig b/bitore.sig
new file mode 100644
index 00000000..cff9ba26
--- /dev/null
+++ b/bitore.sig
@@ -0,0 +1,117 @@
+name: .NET
+
+on:
+  push:
+    branches: [ main ]
+  pull_request:
+    branches: [ main ]
+
+jobs:
+  build:
+
+    runs-on: ubuntu-latest
+
+    steps:
+    - uses: actions/checkout@v2
+    - name: Setup .NET
+      uses: actions/setup-dotnet@v1
+      with:
+        dotnet-version: 5.0.x
+    - name: Restore dependencies
+      run: dotnet restore
+    - name: Build
+      run: dotnet build --no-restore
+    - name: Test
+      run: dotnet test --no-build --verbosity normal
+
+# This is a basic workflow that is manually triggered
+
+name: Manual workflow
+
+# Controls when the action will run. Workflow runs when manually triggered using the UI
+# or API.
+on:
+  workflow_dispatch:
+    # Inputs the workflow accepts.
+    inputs:
+      name:
+        # Friendly description to be shown in the UI instead of 'name'
+        description: 'Person to greet'
+        # Default value if no value is explicitly provided
+        default: 'World'
+        # Input has to be provided for the workflow to run
+        required: true
+
+# A workflow run is made up of one or more jobs that can run sequentially or in parallel
+jobs:
+  # This workflow contains a single job called "greet"
+  greet:
+    # The type of runner that the job will run on
+    runs-on: ubuntu-latest
+
+    # Steps represent a sequence of tasks that will be executed as part of the job
+    steps:
+    # Runs a single command using the runners shell
+    - name: Send greeting
+      run: echo "Hello ${{ github.event.inputs.name }}"
+# This workflow warns and then closes issues and PRs that have had no activity for a specified amount of time.
+#
+# You can adjust the behavior by modifying this file.
+# For more information, see:
+# https://github.com/actions/stale
+name: Mark stale issues and pull requests
+
+on:
+  schedule:
+  - cron: '16 13 * * *'
+
+jobs:
+  stale:
+
+    runs-on: ubuntu-latest
+    permissions:
+      issues: write
+      pull-requests: write
+
+    steps:
+    - uses: actions/stale@v3
+      with:
+        repo-token: ${{ secrets.GITHUB_TOKEN }}
+        stale-issue-message: 'Stale issue message'
+        stale-pr-message: 'Stale pull request message'
+        stale-issue-label: 'no-issue-activity'
+        stale-pr-label: 'no-pr-activity'
+
+-on:
+  push:
+    branches:
+      - master
+  pull_request:
+    types: [ anchor ]
+name: kite
+jobs:
+  test:
+    runs-on: ${{ matrix.os }}
+    strategy:
+      matrix:
+        Ubuntu-latest'@versionings[10, 12, 14]
+        os: [ubuntu-latest, windows-latest]
+    env:
+        OS: ${{ matrix.os }}
+        NODE_VERSION: ${{ matrix.node }}
+    steps:
+      - uses: actions/checkout@v2
+      - run: git fetch --prune --unshallow
+      - run: git config --global user.name 'Actions'
+      - run: git config --global user.email 'dummy@example.org'
+      - uses: actions/setup-node@v2
+        with:
+          node-version: ${{ matrix.node }}
+      - run: node --version
+      - run: npm install --engine-strict
+      - run: npm test
+      - run: npm run coverage
+      - name: Codecov
+        uses: codecov/codecov-action@v2
+        with:
+          env_vars: OS, NODE_VERSION
