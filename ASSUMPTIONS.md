# ASSUMPTIONS.md

## About the users

**Who uses the application?**
The primary user is the new employee — someone who has just joined Meridian and needs
to navigate their first days without being overwhelmed. No admin or HR interface was
built for this MVP; the assumption is that HR (a single person, as stated in the brief)
prepares the data before the new employee's first day.

**What does the user already know when opening the application for the first time?**
The new employee knows:
- Their own name and role (received via email from HR before starting)
- That this application exists and has been shared with them by HR

They do not know:
- Who their colleagues are
- What their first week looks like
- What tasks they are expected to complete during onboarding

---

## About the data

**Who enters the information into the application?**
For this MVP, data is seeded directly into the database by a developer (via the seed
script in `Meridian.Infrastructure/Seed/SeedData.cs`). In a real scenario, HR would
enter or confirm this data before each new hire's start date.

**When is the information added?**
Before the new employee's first working day. The assumption is that HR knows the
employee's role, department, assigned manager, and buddy at least one day in advance.

**What happens if information is missing or incorrect?**
- If `managerId` or `buddyId` is missing, the "Your people" section simply does not
  render those cards — the application handles null values gracefully.
- If checklist items or schedule events are missing, the corresponding sections render
  empty (no crash, no broken UI).
- There is no self-service correction flow in this MVP; incorrect data would need to be
  fixed directly in the database.

---

## About the context

**What device does the new employee use on the first day?**
The assumption is a laptop (provided by the company or personal), running a modern
desktop browser (Chrome, Firefox, Safari, Edge). The application is not optimized for
mobile at this stage, though the layout is reasonably readable on a tablet.

**Do they have access to the application before their first working day?**
This was intentionally left ambiguous. The application does not require authentication,
so technically it could be shared as a link before day one. For this MVP, the assumption
is that HR shares the link on the morning of the first day — no pre-boarding flow was
built.

**Authentication**
Authentication was intentionally omitted. In a 200-person company with 2-3 new hires
per month, the audience for this application is small and well-defined. A shareable
link per employee (containing their ID) would be sufficient for a first version. The
current MVP hardcodes one employee ID for demonstration purposes.
