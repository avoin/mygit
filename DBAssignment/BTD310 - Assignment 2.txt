-- BTD310 - Assignment 2
-- Names 1-3: Barath Kumar, Nitin Prakash, Muhammad Ahsan
-- Student Numbers 1-3: 070439039, 058850108, 016233082
-- Prof: Mehmet Onsekizoglu
-- Date: December 09 2011

-- THIS ASSIGNMENT REPRESENTS OUR GROUP WORK IN ACCORDANCE 
-- WITH SENECA ACADEMIC POLICY.
-- Signatures 1-3: Barath Kumar, Nitin Prakash, Muhammad Ahsan



--#DECLARE CLASS PACKAGE HEADER#

 CREATE OR REPLACE PACKAGE ClassPackage IS

  TYPE t_StudentIDTable IS TABLE OF students.id%TYPE
   INDEX BY BINARY_INTEGER;
 
 e_StudentNotRegistered EXCEPTION;

 
  PROCEDURE UpdateGrade
  (p_dept IN Registered_Students.Department%TYPE,
  p_course IN Registered_Students.Course%TYPE);
  
 PROCEDURE AddRegisteredStudent 
  (p_StudentID IN students.id%TYPE,
  p_dept IN CLASSES.DEPARTMENT%TYPE,
  p_course IN classes.course%TYPE);
  
  PROCEDURE ClassList
  (p_dept IN classes.department%TYPE,
  p_course IN classes.course%TYPE,
  p_IDs OUT t_StudentIDTable,
  p_NumStudents IN OUT BINARY_INTEGER);
  
  
  
  PROCEDURE UpdateStudent
  (p_StudentID IN Students.id%TYPE,
  p_Major IN Students.Major%TYPE);
 
  PROCEDURE RemoveRegisteredStudent
  (p_studentID IN students.id%TYPE,
  p_dept IN classes.department%TYPE,
  p_course IN classes.course%TYPE);
  
  PROCEDURE DeleteStudent
  (p_StudentID IN Students.id%TYPE);
  
  PROCEDURE InsertStudent
  (p_StudentID IN Students.id%TYPE,
  p_FirstName IN Students.First_Name%TYPE,
  p_LastName IN Students.Last_Name%TYPE);
  
  FUNCTION FullName
  (p_StudentID IN students.ID%TYPE)
  RETURN VARCHAR2;
  
  FUNCTION CountCredits
  (p_StudentID IN students.id%TYPE)
  RETURN NUMBER;
END ClassPackage;

--#DECLARE CLASS PACKAGE BODY#

 CREATE OR REPLACE PACKAGE BODY ClassPackage IS

  -- PROCEDURE ADD REGISTERED STUDENT
   PROCEDURE AddRegisteredStudent 
    (p_StudentID IN students.id%TYPE,
    p_dept IN CLASSES.DEPARTMENT%TYPE,
    p_course IN classes.course%TYPE) IS
  
  BEGIN
      INSERT INTO REGISTERED_STUDENTS (STUDENT_ID,DEPARTMENT,COURSE)
      VALUES (p_StudentID, p_dept, p_course);
  END;
  
  --PROCEDURE UPDATE GRADE
   PROCEDURE UpdateGrade
    (p_dept IN Registered_Students.Department%TYPE,
    p_course IN Registered_Students.Course%TYPE) IS
    v_gradeConverted registered_students.grade%TYPE;
   CURSOR c_RegisteredStudents IS
      SELECT *
        FROM his301
        -- only select registered students
          WHERE student_id IN (SELECT student_id
                       FROM registered_students)
          FOR UPDATE OF grade;
  BEGIN 
      FOR v_StudentInfo IN c_RegisteredStudents LOOP
        v_gradeConverted :=
          CASE v_StudentInfo.grade
           WHEN 5 THEN 'A'
           WHEN 4 THEN 'B'
           WHEN 3 THEN 'C'
           WHEN 2 THEN 'D'
           WHEN 1 THEN 'E'
          END;
        UPDATE Registered_Students
          SET grade = v_gradeConverted
        WHERE CURRENT OF c_RegisteredStudents;
      END LOOP;
  END;
  

  --PROCEDURE CLASS LIST
   PROCEDURE ClassList
  (p_dept IN classes.department%TYPE,
  p_course IN classes.course%TYPE,
  p_IDs OUT t_StudentIDTable,
  p_NumStudents IN OUT BINARY_INTEGER) IS
  
  v_count NUMBER(3) :=0;
  CURSOR c_IDs IS
    SELECT student_id
    FROM registered_students
    WHERE course= p_course
    AND department= p_dept;
BEGIN
   OPEN c_Ids;
   LOOP
    EXIT WHEN c_IDs%NOTFOUND;
    FETCH c_IDs INTO p_IDs(v_count);
    v_count := v_count + 1;
    END LOOP;
    CLOSE c_IDs;
    p_NumStudents := v_count;
END;
  

  
  --PROCEDURE UPDATE STUDENT
   PROCEDURE UpdateStudent
    (p_StudentID IN Students.id%TYPE,
    p_Major IN Students.Major%TYPE) IS
  BEGIN
    UPDATE STUDENTS
    SET  major = p_Major
    WHERE ID = p_StudentID;
    IF SQL%NOTFOUND THEN
    RAISE e_StudentNotRegistered;
    END IF;
    
  EXCEPTION
   WHEN e_StudentNotRegistered THEN
     DBMS_OUTPUT.PUT_LINE('STUDENT NOT FOUND!');
   
  END;
  
  -- PROCEDURE INSERT STUDENT
   PROCEDURE InsertStudent
    (p_StudentID IN Students.id%TYPE,
    p_FirstName IN Students.First_Name%TYPE,
    p_LastName IN Students.Last_Name%TYPE) IS
  BEGIN
    INSERT INTO STUDENTS (ID,First_Name,Last_Name)
    VALUES (p_studentID, p_FirstName, p_LastName);
  END;
  
    --Procedure Remove Registered Student
   PROCEDURE RemoveRegisteredStudent
    (p_studentID IN students.id%TYPE,
    p_dept IN classes.department%TYPE,
    p_course IN classes.course%TYPE) IS
  
  BEGIN
   DELETE FROM REGISTERED_STUDENTS
    WHERE student_ID=p_studentID
    AND department = p_dept
    AND course = p_course;
    
    IF SQL%NOTFOUND THEN
    RAISE e_StudentNotRegistered;
    END IF;
    
  EXCEPTION
   WHEN e_StudentNotRegistered THEN
     DBMS_OUTPUT.PUT_LINE('STUDENT NOT FOUND!');
     
  END;
  
  --PROCEDURE DELETE STUDENT
   PROCEDURE DeleteStudent
    (p_StudentID IN Students.id%TYPE) IS
  BEGIN
    DELETE FROM STUDENTS
    WHERE ID = p_StudentID;
    IF SQL%NOTFOUND THEN
    RAISE e_StudentNotRegistered;
    END IF;
    
  EXCEPTION
   WHEN e_StudentNotRegistered THEN
     DBMS_OUTPUT.PUT_LINE('STUDENT NOT FOUND!');
   
  END;
  

  
  
  -- FUNCTION FULL NAME
   FUNCTION FullName
    (p_StudentID IN students.ID%TYPE)
    RETURN VARCHAR2
  IS 
    v_id VARCHAR2(30); -- variable matches return type
  BEGIN
    SELECT first_name || ' ' || last_name
    INTO v_id
    FROM students
    WHERE id= p_StudentID;
    
    IF SQL%NOTFOUND THEN
    RAISE e_StudentNotRegistered;
    END IF;
    
    RETURN v_id;
    
  
  EXCEPTION
   WHEN e_StudentNotRegistered THEN
     DBMS_OUTPUT.PUT_LINE('STUDENT NOT FOUND!');
   
  END FullName;
  
  -- FUNCTION COUNT CREDITS
   FUNCTION CountCredits
   (p_StudentID IN students.id%TYPE)
   RETURN NUMBER
  IS
    v_total number(3) := 0;
    v_gradeConverted number(1);
    CURSOR c_grades IS 
      SELECT grade
      FROM registered_students
      WHERE student_id=p_StudentID;
      
     
      
  BEGIN
    FOR v_StudentInfo IN c_grades LOOP
    
    v_gradeConverted :=
         CASE v_StudentInfo.Grade
           WHEN 'A' THEN 5
           WHEN 'B' THEN 4
           WHEN 'C' THEN 3
           WHEN 'D' THEN 2
           WHEN 'E' THEN 1
          END;
    v_total :=v_total + v_gradeConverted;     
    END LOOP;
    
    IF SQL%NOTFOUND THEN
        RAISE e_StudentNotRegistered;
      END IF;
   
   RETURN v_total;
    
  EXCEPTION
   WHEN e_StudentNotRegistered THEN
     DBMS_OUTPUT.PUT_LINE('STUDENT NOT FOUND!');
   
  END CountCredits;

END ClassPackage;