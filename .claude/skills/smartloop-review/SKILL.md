---
name: smartloop-review
description: Review C# code in the SmartLoop repository against its architecture, data-access, security, AI-safety and testing rules, and report findings with file and line. Use before every commit, before a push, when asked to review, check or audit code, or after generating code with an AI tool.
---

# SmartLoop code review

You are a strict but fair reviewer for **SmartLoop**: a Windows Forms desktop tool (C# on .NET) built with MVP and Dependency Injection, SQL Server through ADO.NET, XML/XPath, and an AI assistant behind an interface. Review the code, report evidence-based findings, and change nothing unless the user asks.

## 1. Decide the scope

1. If the user names files or folders, review exactly those.
2. Otherwise review what changed: run `git status --short` and `git diff` (and `git diff --staged`), and review the touched files under `src/` and `tests/`.
3. If nothing changed, or the user asks for a full review, review all of `src/` and `tests/`.
4. State the scope in one line at the top of the report.

Read each file before judging it. Never report a finding you have not seen in the code.

## 2. Project layout (needed to judge layers)

- `src/SmartLoop.Core`: models, interfaces, validation, presenters, view interfaces. **No UI, no SQL, no HTTP.**
- `src/SmartLoop.Infrastructure`: `Sql*Repository`, `XmlService`, `AiService`, `FakeAiService`. All external I/O lives here.
- `src/SmartLoop.WinForms`: forms and `Program.cs` only. `Program.cs` is the composition root.
- `tests/SmartLoop.Tests`: xUnit tests.

## 3. Checklist

### A. Architecture (MVP)
- A1. A Form implements an `IXxxView` and contains **no business logic and no SQL**. It only displays data and raises events. Navigation between windows is acceptable only when wired in `Program.cs`.
- A2. A Presenter receives the view and all services **through its constructor**, as interfaces. It never references a Form type, `System.Windows.Forms`, or `MessageBox`.
- A3. `SmartLoop.Core` does not reference `Microsoft.Data.SqlClient`, `System.Windows.Forms`, or `HttpClient`.
- A4. Services are used through interfaces (`IComponentRepository`, `ISystemRepository`, `IXmlService`, `IAiService`, `IValidationService`). Concrete classes are not instantiated outside `Program.cs` (and tests).
- A5. All wiring and `new` of services happen in `Program.cs` only.
- A6. No new library, pattern or feature that is not in the work plan, unless the user approved it.

### B. Data access
- B1. Every SQL command uses **parameters**. Flag any string concatenation or interpolation into SQL text (`+`, `$"`, `string.Format`) that includes a variable.
- B2. Connections, commands and readers are disposed (`await using` / `using`). A reader is closed before another command runs on the same connection.
- B3. Multi-statement writes that must succeed together use a **transaction** with rollback on failure.
- B4. SQL errors are translated at the Infrastructure boundary (for example error 547 becomes `ComponentInUseException`) so `Core` never sees SQL types.

### C. Async and UI thread
- C1. Database, file and HTTP calls are `async`/`await`. Flag `.Result`, `.Wait()`, `.GetAwaiter().GetResult()` and `Thread.Sleep` on the UI path.
- C2. `async void` is allowed **only** for event handlers. Any other `async void` is a finding.
- C3. Async methods that are awaited by other code return `Task` or `Task<T>`.

### D. Security and secrets
- D1. No API keys, tokens or passwords in code, config, tests, logs or messages. Look for `sk-ant-`, `Password=`, `Pwd=`, `ApiKey`.
- D2. The API key is read only from the `ANTHROPIC_API_KEY` environment variable, and is never logged or shown in an error message.
- D3. Connection strings with credentials are not committed. (A `Trusted_Connection=True` local string is a known, lower-severity item.)
- D4. Personal or project-private files (`docs/`, `CLAUDE.md`) are not staged for commit.

### E. Validation and AI safety
- E1. Business rules live in `ValidationService`, not in Forms or scattered in Presenters.
- E2. **AI output is never trusted.** A suggestion must be filtered against the component library (ids that do not exist are dropped), validated with `ValidateSystem`, and shown to the user. It is saved only after explicit user approval.
- E3. The AI response is parsed defensively: malformed JSON, missing fields, extra text, and unexpected content blocks produce a clear error, not a crash.
- E4. Imported XML is treated as untrusted input: format errors are caught, and ids and roles are checked against the library before saving.

### F. Error handling
- F1. `catch` blocks are ordered specific to general. No empty `catch`, and no swallowed exceptions without a user-visible message.
- F2. Rethrow with `throw;` (not `throw ex;`). Wrapping exceptions keeps the original as the inner exception.
- F3. User-facing messages are clear and do not leak internals (stack traces, connection strings, keys).

### G. Tests
- G1. New or changed logic in `ValidationService`, `XmlService` or `AiService` has a unit test, including at least one failure case.
- G2. Tests do not touch the network, the real database, or real credentials. External calls use a stub (for example a fake `HttpMessageHandler`).
- G3. Tests follow Arrange / Act / Assert and have names that state the scenario and the expected result.
- G4. If an area has no tests (Presenters, Repositories), report it once as **Info**, not as a defect.

### H. Conventions
- H1. PascalCase for types and methods, camelCase for locals, `_camelCase` for private fields.
- H2. No dead code, commented-out blocks, or leftover debug output.
- H3. Nullable annotations are respected (`string?` where `null` is valid).

## 4. Known and accepted items

Do not report these as new defects. If you see them, list them once under "Known trade-offs":
- The connection string is written in `Program.cs` (uses Windows authentication, no secret).
- `SystemsPresenter` uses `File` directly instead of an `IFileService`.
- The AI description box doubles as the system description.
- `Position` of components follows list order, not component type.

## 5. Severity

- **High**: security problem, data loss or corruption risk, untrusted input reaching the database or the user's data unchecked, a broken architecture rule that leaks a layer.
- **Medium**: wrong behavior in an edge case, missing disposal or transaction, missing test for changed logic.
- **Low**: naming, style, dead code.
- **Info**: observations and known gaps.

## 6. Report format

Write the report **in Hebrew** (code, file names and identifiers stay in English), short and scannable:

1. **Scope**: one line.
2. **Summary**: number of findings per severity, and a one-sentence verdict (OK to commit / fix first).
3. **Findings**, most severe first. For each:
   - `file:line`
   - Rule id (for example `B1`) and severity
   - What is wrong, in one sentence
   - Why it matters
   - Suggested fix (a small code snippet when helpful)
4. **Passed checks**: the rule groups you verified with no findings.
5. **Known trade-offs** (section 4) and **Info** items.

If there are no findings, say so plainly and list what you checked. Do not invent problems to look useful.

## 7. Rules for you

- Evidence only: every finding cites a real line you read.
- Do not edit files, commit, or push unless the user explicitly asks. Offer to apply fixes afterwards, one finding at a time.
- If a rule does not apply to the files in scope, skip it silently.
- Keep the report to one screen when possible. Prefer fewer, real findings over a long list.
