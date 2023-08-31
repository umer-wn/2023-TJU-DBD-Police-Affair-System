--��salary��policemenת��employ��--
ALTER TABLE policemen
DROP COLUMN salary;

ALTER TABLE employ
ADD  salary varchar(20) DEFAULT ��5000��;

--�½����������ԣ�������ˮ�š������˾��š����������ž��֡���н�ա��������ʡ�����--
CREATE TABLE payroll_stub (
  payroll_number VARCHAR(20) PRIMARY KEY,
  police_number VARCHAR(20),
  police_name VARCHAR(20),
  STATION_ID VARCHAR(20), 
  pay_day DATE,
  salary VARCHAR(6),
  subsidy VARCHAR(6),
  FOREIGN KEY (STATION_ID) REFERENCES police_station(STATION_ID),
  FOREIGN KEY (police_number) REFERENCES policemen(police_number)
);
-- �����ݴӾɱ�����±�

INSERT INTO payroll_stub (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy)VALUES(1, '3889043', '����Ӣ', '130101005', '1-1�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(2, '3889043', '����Ӣ', '130101005', '1-2�� -98','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(3, '9836582', '��ΰ', '120102004', '1-6�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(4, '9836582', '��ΰ', '120102004', '21-7�� -08','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(5, '1691715', '����', '110102002', '05-3�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(6, '1691715', '����', '110102002', '29-4�� -03','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(7, '0975704', '����', '110101001', '18-8�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(8, '0975704', '����', '110101001', '03-10��-02','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(9, '0715805', '����', '320101006', '1-7�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(10, '0715805', '����', '320101006', '13-8�� -12','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(11, '4264114', '����', '440102007', '1-11��-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(12, '4264114', '����', '440102007', '29-5�� -10','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(13, '8837988', '�ž�', '510101008', '21-2�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(14, '8837988', '�ž�', '510101008', '17-12��-90','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(15, '6827713', '����', '320102009', '17-4�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(16, '6827713', '����', '320102009', '23-2�� -01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(17, '4348435', '����', '330101010', '23-9�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(18, '4348435', '����', '330101010', '14-6�� -06','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(19, '9234086', '����', '610101011', '08-5�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(20, '9234086', '����', '610101011', '27-3�� -95','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(21, '8268264', '����', '330102012', '08-12��-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(22, '8268264', '����', '330102012', '30-9�� -02','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(23, '9173384', '����', '440101013', '01-7�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(24, '9173384', '����', '440101013', '08-5�� -91','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(25, '5218977', '����', '330103015', '03-1�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(26, '5218977', '����', '330103015', '02-4�� -96','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(27, '7614546', '����', '440103016', '06-2�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(28, '7614546', '����', '440103016', '21-8�� -98','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(29, '4789420', '����', '510102017', '19-5�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(30, '4789420', '����', '510102017', '10-6�� -86','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(31, '4693712', '�Ծ�', '610102018', '27-3�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(32, '4693712', '�Ծ�', '610102018', '25-10��-93','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(33, '2085174', '��ΰ', '440105026', '29-6�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(34, '8712011', '����', '130101005', '11-9�� -95','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(35, '6916120', '����', '330102012', '19-10��-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(36, '6916120', '����', '120102004', '03-4�� -98','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(37, '9231816', '����', '110102002', '14-2�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(38, '9231816', '����', '110102002', '19-5�� -04','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(39, '0956317', '��ϼ', '440101013', '17-6�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(40, '0956317', '��ϼ', '110101001', '30-10��-01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(41, '8988132', '��ƽ', '320103014', '16-7�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(42, '5480803', '����', '330105021', '04-9�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(43, '1301569', '����', '610104028', '18-1�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(44, '0763368', '�¾�', '320104019', '30-5�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(45, '6586238', '��ΰ', '330104020', '11-12��-22','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(46, '3468353', '����', '440103016', '03-7�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(47, '0843435', '����', '510101008', '23-7�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(48, '8734165', '�½�', '320104019', '03-10��-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(49, '8734165', '�½�', '320104019', '22-9�� -87','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(50, '0664786', '����', '330104020', '08-4�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(51, '0664786', '����', '330104020', '15-1�� -90','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(52, '9884669', '����', '330105021', '15-6�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(53, '9884669', '����', '330105021', '18-11��-01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(54, '7104084', '����', '440104022', '14-7�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(55, '7104084', '����', '440104022', '05-7�� -04','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(56, '3242355', '�¾�', '510103023', '08-9�� -22','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(57, '3242355', '�¾�', '510103023', '08-3�� -99','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(58, '2402834', '��ΰ', '610103024', '20-7�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(59, '2402834', '��ΰ', '610103024', '19-5�� -94','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(60, '5639807', '����', '320105025', '31-1�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(61, '5639807', '����', '320105025', '28-4�� -85','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(62, '8167027', '����', '440105026', '25-2�� -23','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(63, '8167027', '����', '440105026', '13-10��-01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(64, '7233141', '����', '510104027', '05-6�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(65, '7233141', '����', '510104027', '16-6�� -03','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(66, '7003217', '����', '320106029', '28-8�� -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(67, '7003217', '����', '320106029', '02-7�� -88','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(68, '1313149', '�¾�', '330106030', '25-5�� -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(69, '1313149', '�¾�', '330106030', '23-2�� -99','5000','100');