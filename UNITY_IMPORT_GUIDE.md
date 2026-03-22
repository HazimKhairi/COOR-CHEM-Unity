# Unity Import Progress Guide

## Where to Find Progress Bar

**Location:** Unity Editor bottom-right corner

```
┌─────────────────────────────────────────────────────────┐
│ Unity Editor Window                                     │
│                                                          │
│  [Scene View]                    [Game View]            │
│                                                          │
│                                                          │
│  [Console] [Project] [Inspector]                        │
│                                                          │
└──────────────────────────────────────────────[Progress]─┘
                                                     ↑
                                            Progress bar here
```

## What You'll See

When Unity imports new assets:
```
Importing... (12/15) Assets/Textures/Logo-4.png
[████████████░░░░░░] 80%
```

## Status Messages

✅ **"Import complete"** - All good, no errors
⚠️ **"Importing..."** - Wait for it to finish
❌ **"Failed to import"** - Check Console for errors

## If No Progress Bar Shows

Unity might have already imported. Check:
1. **Project window** → Assets/Textures → files ada thumbails ke?
2. **Console window** → ada error messages ke?

## Keyboard Shortcut

- **Cmd+0** (Mac) / **Ctrl+0** (Windows) - Toggle Console
- **Cmd+5** (Mac) / **Ctrl+5** (Windows) - Toggle Project window

---

If stuck at "Importing..." for > 2 minutes:
- Check Activity Monitor - Unity process still running?
- Force reimport: Right-click folder → Reimport
