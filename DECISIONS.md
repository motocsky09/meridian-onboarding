# DECISIONS.md

## Product decisions

**Which features did you include?**

1. **Onboarding dashboard** — the core screen a new employee sees on day one: who their manager and buddy are, a checklist of tasks grouped by stage (Day 1, Week 1, Month 1), and a schedule of their first week including office/remote days and key meetings.

2. **Interactive checklist** — tasks can be marked as completed with a single click. The state is persisted in MongoDB, so progress is not lost on page refresh.

3. **Team directory** — a browsable list of all 17 Meridian employees, grouped by department, so the new employee can orient themselves in the organization.

**How did you prioritize them?**

The onboarding dashboard was the obvious starting point — it directly addresses the core problem stated in the brief ("you don't know anyone, you don't know how things work"). The interactive checklist came next because a static checklist loses most of its value; being able to track progress makes the feature genuinely useful. The team directory was added third because knowing who your colleagues are is the next most pressing need after knowing your own schedule.

**Which features did you intentionally leave out of scope?**

- **HR admin interface** (adding/editing employees, checklist items, or schedule events through a UI). For 2-3 new hires per month, running a seed script is acceptable; a full CRUD interface would have cost more than the remaining features were worth for an MVP.
- **Authentication / login**. The brief states auth is optional. The current version hardcodes one employee ID to keep the focus on the onboarding experience itself.
- **Employee selector / multi-user view**. The dashboard shows a single hardcoded employee. Supporting "log in as any employee" would be a small change but was deprioritized in favor of making the single-user experience complete.

---

## Technical decisions

**Why did you choose this database structure?**

Three MongoDB collections: `employees`, `checklistItems`, and `scheduleEvents`. Checklist items and schedule events reference an employee via `employeeId`. This keeps each new employee's onboarding data isolated and easy to query with a single filter. Employees reference their manager and buddy by ID (`managerId`, `buddyId`), which models the organizational relationships without duplicating data.

Departments are stored as a plain string field on the employee rather than as a separate collection — with only five fixed departments, a separate collection would have added joins and complexity without real benefit.

**Why did you choose these libraries/frameworks?**

- **.NET 8 (C#)** — a stack I am already comfortable with, which let me move quickly. .NET 8 is an LTS release, so it is stable and well-supported.
- **3-layer architecture (Api / Application / Infrastructure)** — a simplified Clean Architecture. I merged the Domain layer into Application because the domain is small (three entities) and separate repository interfaces in a dedicated Domain project would have been ceremony without payoff. I kept Infrastructure isolated so that MongoDB-specific details never leak into the business models — the domain models are plain C# classes with no Mongo attributes.
- **Angular 16 (standalone components)** — standalone components avoid NgModule boilerplate, which suits a small project. Angular pairs naturally with a TypeScript-first frontend and gives structure out of the box (routing, DI, services).
- **MongoDB** — a document database fits onboarding data well: each employee's checklist and schedule are naturally document-shaped, and the flexible schema made iterating on the data model fast.

**A note on the combined dashboard endpoint**

Rather than exposing separate endpoints for employee, checklist, and schedule, I built a single `GET /api/employees/{id}/dashboard` endpoint that returns everything the dashboard screen needs in one response, with manager/buddy names resolved server-side and the checklist grouped by stage. This means the frontend makes one request instead of three or four, and the flattening logic lives in one place on the backend.

**If you had more time, what would you build differently?**

- The seed data generates schedule days as `startDate + N days` without skipping weekends, so "Remote day" can fall on a Saturday/Sunday. I would generate only working days.
- I would replace the hardcoded employee ID with a proper per-employee link.

---

## UX decisions

**Why did you choose this user flow?**

The application opens directly on the new employee's dashboard — no login, no landing page, no navigation to click through. The reasoning is that a new employee on day one is already overwhelmed; the fewer steps between opening the link and seeing "here is your first week and here is who to talk to," the better. The team directory is one click away, not front and center, because it is a secondary need.

**Did you test it with anyone?**

Not formally. The flow was validated against the brief's scenario (a new hire who knows nothing on day one) rather than with real users, given the time constraints.

**What changed after receiving feedback?**

The single change of direction worth noting is the checklist: it started as a static, read-only list. It became clear during development that a checklist you cannot interact with provides little value over a printed sheet, so I added the toggle-and-persist behavior. This is documented here because the brief explicitly values honest documentation of changed direction.
