# CKLLib

**The library for describing and using the algebra of dynamic relations and its operations.**

---

## Table of Contents
- [CKLLib](#ckllib)
  - [Table of Contents](#table-of-contents)
  - [Overview](#overview)
  - [Core Classes](#core-classes)
    - [CKL](#ckl)
    - [RelationItem](#relationitem)
    - [TimeInterval](#timeinterval)
    - [Pair](#pair)
  - [Enums](#enums)
    - [TimeDimentions](#timedimentions)
  - [Utility Classes](#utility-classes)
    - [CKLMath](#cklmath)
    - [CKLGraph](#cklgraph)
  - [References](#references)

---

## Overview
CKLLib is a powerful library designed to model and manipulate dynamic relations with temporal attributes. It provides a comprehensive set of classes and operations to handle time-dependent relational data efficiently.

---

## Core Classes

### CKL
The main class representing a dynamic relation object.

**Attributes:**
| Attribute        | Description                                                           |
| ---------------- | --------------------------------------------------------------------- |
| `FilePath`       | The file path (with `.ckl` extension) where the object is serialized. |
| `GlobalInterval` | The time interval over which the object is considered.                |
| `TimeDimention`  | The time units of the object (e.g., minutes, nanoseconds, days).      |
| `Source`         | A set (Cartesian product of sets) of which the relation is a subset.  |
| `Relation`       | The relation proper.                                                  |

---

### RelationItem
Describes an element of a dynamic relation.

**Attributes:**
| Attribute   | Description                                                                           |
| ----------- | ------------------------------------------------------------------------------------- |
| `Value`     | The value of the element (retrieved from `Source`).                                   |
| `Intervals` | Validity intervals for the element (time intervals where it is part of the relation). |
| `Info`      | Optional object attribute.                                                            |

---

### TimeInterval
Represents a time interval.

**Attributes:**
| Attribute   | Description                       |
| ----------- | --------------------------------- |
| `StartTime` | Lower bound of the time interval. |
| `EndTime`   | Upper bound of the time interval. |
| `Duration`  | The duration of the interval.     |

---

### Pair
Represents the value of a `RelationItem`.

**Attributes:**
| Attribute | Description                  |
| --------- | ---------------------------- |
| `Values`  | A tuple of concrete objects. |

---

## Enums

### TimeDimentions
An enum of time units.

**Values:**
- Nanoseconds
- Microseconds
- Milliseconds
- Seconds
- Minutes
- Hours
- Days
- Weeks

---

## Utility Classes

### CKLMath
Static operations for dynamic relation algebra (similar to `Math` for calculus).

**Note:**  
For detailed information about operations and their subtypes, refer to the literature listed in the [References](#references) section.

---

### CKLGraph
Provides a temporal graph representation of a dynamic relation.

**Methods:**
| Method              | Description  |
| ------------------- | ------------ |
| `GetGraphByTime(int | float time)` | Returns a `HashSet<Pair>` representing the edge list of the graph at the given timestamp. |

---

## References
- *Гейда А. С., Федорченко Л. Н., Афанасьева И. В., Хасанов Д. С. Динамические отношения в задачах обработки знаний // Вестник Бурятского государственного университета. Математика, информатика. 2021. № 3. С. 39–61.*
- *Федорченко Л. Н., Гейда А. С. Инструментальная система обработки динамических отношений // Вестник Бурятского государственного университета. Математика, информатика. 2022. № 2. С. 102–111.*
- *В. И. Городецкий, Л. Н. Федорченко - "Динамические отношения в задачах обработки знаний."// Академия наук СССР Ленинградский институт информатики и автоматизации, Препринт №141, Изд-во. Ленинград. 1991*

