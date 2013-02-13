/*
 * Created by JFormDesigner on Mon Apr 21 12:50:34 EDT 2008
 */

package Provider.GoogleMapsStatic.TestUI;

import Provider.GoogleMapsStatic.*;
import Task.*;
import Task.Manager.*;
import Task.ProgressMonitor.*;
import Task.Support.CoreSupport.*;
import Task.Support.GUISupport.*;
import com.jgoodies.forms.factories.*;
import info.clearthought.layout.*;
import org.apache.commons.httpclient.*;
import org.apache.commons.httpclient.methods.*;

import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;

import javax.imageio.*;
import javax.swing.*;
import javax.swing.border.*;
import java.awt.*;
import java.awt.event.*;
import java.awt.image.*;
import java.beans.*;
import java.text.*;
import java.util.concurrent.*;

/** @author nazmul idris */
/* edited for academic use by Barath Kumar */
public class SampleApp extends JFrame {
//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// data members
//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
/** reference to task */
private SimpleTask _task;
/** this might be null. holds the image to display in a popup */
private BufferedImage _img;
/** this might be null. holds the text in case image doesn't display */
private String _respStr;

//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// main method...
//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

public static void main(String[] args) {
  Utils.createInEDT(SampleApp.class);
}

//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// constructor
//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

private void doInit() {
  GUIUtils.setAppIcon(this, "burn.png");
  GUIUtils.centerOnScreen(this);
  setVisible(true);

  int W = 28, H = W;
  boolean blur = false;
  float alpha = .7f;

  try {
    btnGetMap.setIcon(ImageUtils.loadScaledBufferedIcon("ok1.png", W, H, blur, alpha));
    btnQuit.setIcon(ImageUtils.loadScaledBufferedIcon("charging.png", W, H, blur, alpha));
  }
  catch (Exception e) {
    System.out.println(e);
  }

  _setupTask();
}

/** create a test task and wire it up with a task handler that dumps output to the textarea */
@SuppressWarnings("unchecked")
private void _setupTask() {

  TaskExecutorIF<ByteBuffer> functor = new TaskExecutorAdapter<ByteBuffer>() {
    public ByteBuffer doInBackground(Future<ByteBuffer> swingWorker,
                                     SwingUIHookAdapter hook) throws Exception
    {

      _initHook(hook);

      String uri = MapLookup.getMap(Double.parseDouble(ttfLat.getText()),
                                    Double.parseDouble(ttfLon.getText()),
                                    Integer.parseInt(ttfSizeW.getText()),
                                    Integer.parseInt(ttfSizeH.getText()),
                                    Integer.parseInt(ttfZoom.getText())
      );
      sout("Google Maps URI=" + uri);

      // get the map from Google
      // custom get method parameters to bypass mapLookup because it does not add map type to the uri
      GetMethod get = new GetMethod(uri+"&maptype="+maptype);
      
      new HttpClient().executeMethod(get);

      ByteBuffer data = HttpUtils.getMonitoredResponse(hook, get);

      try {
        _img = ImageUtils.toCompatibleImage(ImageIO.read(data.getInputStream()));
        sout("converted downloaded data to image...");
      }
      catch (Exception e) {
        _img = null;
        sout("The URI is not an image. Data is downloaded, can't display it as an image.");
        _respStr = new String(data.getBytes());
      }

      return data;
    }

    @Override public String getName() {
      return _task.getName();
    }
  };

  _task = new SimpleTask(
      new TaskManager(),
      functor,
      "HTTP GET Task",
      "Download an image from a URL",
      AutoShutdownSignals.Daemon
  );

  _task.addStatusListener(new PropertyChangeListener() {
    public void propertyChange(PropertyChangeEvent evt) {
      sout(":: task status change - " + ProgressMonitorUtils.parseStatusMessageFrom(evt));
      lblProgressStatus.setText(ProgressMonitorUtils.parseStatusMessageFrom(evt));
    }
  });

  _task.setTaskHandler(new
      SimpleTaskHandler<ByteBuffer>() {
        @Override public void beforeStart(AbstractTask task) {
          sout(":: taskHandler - beforeStart");
        }
        @Override public void started(AbstractTask task) {
          sout(":: taskHandler - started ");
        }
        /** {@link SampleApp#_initHook} adds the task status listener, which is removed here */
        @Override public void stopped(long time, AbstractTask task) {
          sout(":: taskHandler [" + task.getName() + "]- stopped");
          sout(":: time = " + time / 1000f + "sec");
          task.getUIHook().clearAllStatusListeners();
        }
        @Override public void interrupted(Throwable e, AbstractTask task) {
          sout(":: taskHandler [" + task.getName() + "]- interrupted - " + e.toString());
        }
        @Override public void ok(ByteBuffer value, long time, AbstractTask task) {
          sout(":: taskHandler [" + task.getName() + "]- ok - size=" + (value == null
              ? "null"
              : value.toString()));
          if (_img != null) {
            _displayImgInFrame();
          }
          else _displayRespStrInFrame();

        }
        @Override public void error(Throwable e, long time, AbstractTask task) {
          sout(":: taskHandler [" + task.getName() + "]- error - " + e.toString());
        }
        @Override public void cancelled(long time, AbstractTask task) {
          sout(" :: taskHandler [" + task.getName() + "]- cancelled");
        }
      }
  );
}

private SwingUIHookAdapter _initHook(SwingUIHookAdapter hook) {
  hook.enableRecieveStatusNotification(checkboxRecvStatus.isSelected());
  hook.enableSendStatusNotification(checkboxSendStatus.isSelected());

  hook.setProgressMessage(ttfProgressMsg.getText());

  PropertyChangeListener listener = new PropertyChangeListener() {
    public void propertyChange(PropertyChangeEvent evt) {
      SwingUIHookAdapter.PropertyList type = ProgressMonitorUtils.parseTypeFrom(evt);
      int progress = ProgressMonitorUtils.parsePercentFrom(evt);
      String msg = ProgressMonitorUtils.parseMessageFrom(evt);

      progressBar.setValue(progress);
      progressBar.setString(type.toString());

      sout(msg);
    }
  };

  hook.addRecieveStatusListener(listener);
  hook.addSendStatusListener(listener);
  hook.addUnderlyingIOStreamInterruptedOrClosed(new PropertyChangeListener() {
    public void propertyChange(PropertyChangeEvent evt) {
      sout(evt.getPropertyName() + " fired!!!");
    }
  });

  return hook;
}

private void _displayImgInFrame() {

  final JFrame frame = new JFrame("Google Static Map");
  GUIUtils.setAppIcon(frame, "71.png");
  frame.setDefaultCloseOperation(DISPOSE_ON_CLOSE);

  JLabel imgLbl = new JLabel(new ImageIcon(_img));
  imgLbl.setToolTipText(MessageFormat.format("<html>Image downloaded from URI<br>size: w={0}, h={1}</html>",
                                             _img.getWidth(), _img.getHeight()));
  imgLbl.addMouseListener(new MouseListener() {
    public void mouseClicked(MouseEvent e) {}
    public void mousePressed(MouseEvent e) { frame.dispose();}
    public void mouseReleased(MouseEvent e) { }
    public void mouseEntered(MouseEvent e) { }
    public void mouseExited(MouseEvent e) { }
  });

	imgLbl.addMouseWheelListener(new MouseWheelListener() {

	@Override
	public void mouseWheelMoved(MouseWheelEvent e) {
		if (e.getWheelRotation() > 0)
			zoomSlider.setValue(zoomSlider.getValue() + 1);
		else
			zoomSlider.setValue(zoomSlider.getValue() - 1);
	}

	});
	//adding map inside main frame
  mPanel1.removeAll();
  mPanel1.add(imgLbl);
  mPanel1.repaint();
  
}

private void _displayRespStrInFrame() {

  final JFrame frame = new JFrame("Google Static Map - Error");
  GUIUtils.setAppIcon(frame, "69.png");
  frame.setDefaultCloseOperation(DISPOSE_ON_CLOSE);

  JTextArea response = new JTextArea(_respStr, 25, 80);
  response.addMouseListener(new MouseListener() {
    public void mouseClicked(MouseEvent e) {}
    public void mousePressed(MouseEvent e) { frame.dispose();}
    public void mouseReleased(MouseEvent e) { }
    public void mouseEntered(MouseEvent e) { }
    public void mouseExited(MouseEvent e) { }
  });

  frame.setContentPane(new JScrollPane(response));
  frame.pack();

  GUIUtils.centerOnScreen(frame);
  frame.setVisible(true);
}

/** simply dump status info to the textarea */
private void sout(final String s) {
  Runnable soutRunner = new Runnable() {
    public void run() {
      if (ttaStatus.getText().equals("")) {
        ttaStatus.setText(s);
      }
      else {
        ttaStatus.setText(ttaStatus.getText() + "\n" + s);
      }
    }
  };

  if (ThreadUtils.isInEDT()) {
    soutRunner.run();
  }
  else {
    SwingUtilities.invokeLater(soutRunner);
  }
}

private void startTaskAction() {
  try {
    _task.execute();
  }
  catch (TaskException e) {
    sout(e.getMessage());
  }
}


public SampleApp() {
  initComponents();
  doInit();
}

private void quitProgram() {
  _task.shutdown();
  System.exit(0);
}

private void initComponents() {
  // JFormDesigner - Component initialization - DO NOT MODIFY  //GEN-BEGIN:initComponents
  // Generated using JFormDesigner non-commercial license
	dialogPane = new JPanel();
	contentPanel = new JPanel();
	
	//zoom functionality added
	zoomPanel = new JPanel();
	zoomSlider = new JSlider(JSlider.HORIZONTAL, ZOOM_MIN, ZOOM_MAX,
				ZOOM_INIT);
				
	dialogScroll = new JScrollPane();
	
	//map functionality added
	mPanel1 = new JPanel();
	mapContent = new JPanel();
	mapOptions = new JPanel();
	
	//panning buttons added
	btnup = new JButton("UP");
	btndown = new JButton("DOWN");
	btnleft = new JButton("LEFT");
	btnright = new JButton("RIGHT");
	panning = new JPanel();
  
  panel1 = new JPanel();
  label2 = new JLabel();
  ttfSizeW = new JTextField();
  label4 = new JLabel();
  ttfLat = new JTextField();
  btnGetMap = new JButton();
  label3 = new JLabel();
  ttfSizeH = new JTextField();
  label5 = new JLabel();
  ttfLon = new JTextField();
  btnQuit = new JButton();
  label1 = new JLabel();
  label6 = new JLabel();
  ttfZoom = new JTextField();
  scrollPane1 = new JScrollPane();
  ttaStatus = new JTextArea();
  panel2 = new JPanel();
  panel3 = new JPanel();
  checkboxRecvStatus = new JCheckBox();
  checkboxSendStatus = new JCheckBox();
  ttfProgressMsg = new JTextField();
  progressBar = new JProgressBar();
  lblProgressStatus = new JLabel();
  
  //map type panel added
  maptypepanel = new JPanel();
  
  //map type added
  jcmaptype = new JComboBox();
  jcmaptype.insertItemAt("Road", 0);
  jcmaptype.insertItemAt("Satellite", 1);
  jcmaptype.insertItemAt("Hybrid", 2);
  jcmaptype.setSelectedIndex(0);

  //======== this ========
  setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
  setTitle("Google Static Maps");
  setIconImage(null);
  Container contentPane = getContentPane();
  contentPane.setLayout(new BorderLayout());

  //======== dialogPane ========
  {
  	dialogPane.setBorder(new EmptyBorder(12, 12, 12, 12));
  	dialogPane.setOpaque(false);
  	dialogPane.setLayout(new BorderLayout());

  	//======== contentPanel ========
  	{
  		contentPanel.setOpaque(false);
  		contentPanel.setLayout(new TableLayout(new double[][] {
  			{TableLayout.FILL},
  			{TableLayout.PREFERRED, TableLayout.FILL, TableLayout.PREFERRED}}));
  		((TableLayout)contentPanel.getLayout()).setHGap(5);
  		((TableLayout)contentPanel.getLayout()).setVGap(5);
		//===== mapArea =====
		{
			mapContent.setLayout(new GridLayout(1, 0));
			mapContent.setMaximumSize(new Dimension(600, 600));
		}
  		//======== panel1 ========
  		{
  			panel1.setOpaque(false);
  			panel1.setBorder(new CompoundBorder(
  				new TitledBorder("Configure the inputs to Google Static Maps"),
  				Borders.DLU2_BORDER));
  			panel1.setLayout(new TableLayout(new double[][] {
  				{0.17, 0.17, 0.17, 0.17, 0.05, TableLayout.FILL},
  				{TableLayout.PREFERRED, TableLayout.PREFERRED, TableLayout.PREFERRED}}));
  			((TableLayout)panel1.getLayout()).setHGap(5);
  			((TableLayout)panel1.getLayout()).setVGap(5);
			
			//---- mPanel1 ----
			mPanel1.setOpaque(false);
			mPanel1.setLayout(new GridBagLayout());
			mPanel1.setBorder(new CompoundBorder(new TitledBorder(
					"Map"), Borders.DLU2_BORDER));

			//custom map ui added to the right side of the map panel
			mapOptions.setOpaque(false);
			mapOptions.setBorder(new CompoundBorder(new TitledBorder(
					"Controls"), Borders.DLU2_BORDER));
			mapOptions.setLayout(new BorderLayout());
			
			Box controlBox = Box.createHorizontalBox(), PanningBox = Box
							.createHorizontalBox(), zoomBox = Box
							.createHorizontalBox(), maptypepanelBox = Box
							.createHorizontalBox(), verticalBox = Box
							.createVerticalBox();
			
			//adding map type to the uri look up method using listener
			jcmaptype.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					JComboBox cb = (JComboBox) e.getSource();
					//will send maptype based on selection done by user
					if (jcmaptype.getSelectedIndex() >= 0) {
						if(jcmaptype.getSelectedIndex() == 0){
							
							//3 different map types supported
							maptype = "roadmap";
							}
							else if(jcmaptype.getSelectedIndex() == 1){
							maptype = "satellite";
							}
							else if(jcmaptype.getSelectedIndex() == 2){
							maptype = "hybrid";
							}
						}
					}
				}
			);
			
			panning.setLayout(new TableLayout(new double[][] {
							{ TableLayout.FILL, TableLayout.FILL,
									TableLayout.FILL },
							{ TableLayout.FILL, TableLayout.FILL,
									TableLayout.FILL } }));

					PanningActionBtn btnHandler = new PanningActionBtn();

					//adding panning controls using buttons
					btnup.addActionListener(btnHandler);
					btnleft.addActionListener(btnHandler);
					btnright.addActionListener(btnHandler);
					btndown.addActionListener(btnHandler);
					
					panning.add(btnup, "1, 0");
					panning.add(btnleft, "0, 1");
					panning.add(btnright, "2, 1");
					panning.add(btndown, "1, 2");

					panning.setBorder(new CompoundBorder(
							new TitledBorder("Pan"), Borders.DLU2_BORDER));

					PanningBox.add(panning);
					PanningBox.setMaximumSize(new Dimension(300, 100));

					zoomSlider.setMajorTickSpacing(2);

					zoomSlider.setSize(500, 25);

					zoomSlider.setPreferredSize(new Dimension(290, 30));
					zoomSlider.setPaintTicks(true);

					/*adding zoom functionality using a slider via listener 
					updating zoom level in the existing text box*/
					zoomSlider.addChangeListener(new ChangeListener() {
						public void stateChanged(ChangeEvent e) {
							int zoom = ((JSlider) e.getSource()).getValue();

							ttfZoom.setText(String.format("%d", zoom));
							startTaskAction();
						}
					});
					
					zoomPanel.setBorder(new CompoundBorder(new TitledBorder(
							"Zoom"), Borders.DLU2_BORDER));

					zoomPanel.setMaximumSize(new Dimension(300, 100));
					zoomPanel.add(zoomSlider);

					zoomBox.add(zoomPanel);
					
					maptypepanel.add(jcmaptype);

					maptypepanel.setBorder(new CompoundBorder(
							new TitledBorder("Map Type"), Borders.DLU2_BORDER));

					maptypepanelBox.add(maptypepanel);
					maptypepanelBox.setMaximumSize(new Dimension(300, 100));
					
					//adding map functionality in a neat top down order format
					verticalBox.add(controlBox);
					verticalBox.add(Box.createVerticalStrut(10));
					verticalBox.add(PanningBox);
					verticalBox.add(Box.createVerticalStrut(10));
					verticalBox.add(zoomBox);
					verticalBox.add(Box.createVerticalStrut(10));
					verticalBox.add(maptypepanelBox);
					
					mapOptions.add(verticalBox, BorderLayout.CENTER);
			
  			//---- label2 ----
  			label2.setText("Size Width");
  			label2.setHorizontalAlignment(SwingConstants.RIGHT);
  			panel1.add(label2, new TableLayoutConstraints(0, 0, 0, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- ttfSizeW ----
  			ttfSizeW.setText("512");
  			panel1.add(ttfSizeW, new TableLayoutConstraints(1, 0, 1, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- label4 ----
  			label4.setText("Latitude");
  			label4.setHorizontalAlignment(SwingConstants.RIGHT);
  			panel1.add(label4, new TableLayoutConstraints(2, 0, 2, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- ttfLat ----
  			ttfLat.setText("38.931099");
  			panel1.add(ttfLat, new TableLayoutConstraints(3, 0, 3, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- btnGetMap ----
  			btnGetMap.setText("Get Map");
  			btnGetMap.setHorizontalAlignment(SwingConstants.LEFT);
  			btnGetMap.setMnemonic('G');
  			btnGetMap.addActionListener(new ActionListener() {
  				public void actionPerformed(ActionEvent e) {
  					startTaskAction();
  				}
  			});
  			panel1.add(btnGetMap, new TableLayoutConstraints(5, 0, 5, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- label3 ----
  			label3.setText("Size Height");
  			label3.setHorizontalAlignment(SwingConstants.RIGHT);
  			panel1.add(label3, new TableLayoutConstraints(0, 1, 0, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- ttfSizeH ----
  			ttfSizeH.setText("512");
  			panel1.add(ttfSizeH, new TableLayoutConstraints(1, 1, 1, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- label5 ----
  			label5.setText("Longitude");
  			label5.setHorizontalAlignment(SwingConstants.RIGHT);
  			panel1.add(label5, new TableLayoutConstraints(2, 1, 2, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- ttfLon ----
  			ttfLon.setText("-77.3489");
  			panel1.add(ttfLon, new TableLayoutConstraints(3, 1, 3, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- btnQuit ----
  			btnQuit.setText("Quit");
  			btnQuit.setMnemonic('Q');
  			btnQuit.setHorizontalAlignment(SwingConstants.LEFT);
  			btnQuit.setHorizontalTextPosition(SwingConstants.RIGHT);
  			btnQuit.addActionListener(new ActionListener() {
  				public void actionPerformed(ActionEvent e) {
  					quitProgram();
  				}
  			});
  			panel1.add(btnQuit, new TableLayoutConstraints(5, 1, 5, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- label6 ----
  			label6.setText("Zoom");
  			label6.setHorizontalAlignment(SwingConstants.RIGHT);
  			panel1.add(label6, new TableLayoutConstraints(2, 2, 2, 2, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- ttfZoom ----
  			ttfZoom.setText("14");
  			panel1.add(ttfZoom, new TableLayoutConstraints(3, 2, 3, 2, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));
  		}
  		contentPanel.add(panel1, new TableLayoutConstraints(0, 0, 0, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));
		
  		//more map stuff
  		//defining map and map control panel specs
			mPanel1.setMaximumSize(new Dimension(550, 550));
			mPanel1.setPreferredSize(new Dimension(530, 530));
			mPanel1.setMinimumSize(new Dimension(525, 525));
			mapOptions.setPreferredSize(new Dimension(350, 350));
			mapOptions.setMinimumSize(new Dimension(350, 350));
			mapOptions.setMaximumSize(new Dimension(350, 350));
			mapContent.add(mPanel1);
			mapContent.add(mapOptions);
  		//======== scrollPane1 ========
  		{
  			scrollPane1.setBorder(new TitledBorder("System.out - displays all status and progress messages, etc."));
  			scrollPane1.setOpaque(false);

  			//---- ttaStatus ----
  			ttaStatus.setBorder(Borders.createEmptyBorder("1dlu, 1dlu, 1dlu, 1dlu"));
  			ttaStatus.setToolTipText("<html>Task progress updates (messages) are displayed here,<br>along with any other output generated by the Task.<html>");
  			scrollPane1.setViewportView(ttaStatus);
  		}
  		contentPanel.add(scrollPane1, new TableLayoutConstraints(0, 1, 0, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  		//======== panel2 ========
  		{
  			panel2.setOpaque(false);
  			panel2.setBorder(new CompoundBorder(
  				new TitledBorder("Status - control progress reporting"),
  				Borders.DLU2_BORDER));
  			panel2.setLayout(new TableLayout(new double[][] {
  				{0.45, TableLayout.FILL, 0.45},
  				{TableLayout.PREFERRED, TableLayout.PREFERRED}}));
  			((TableLayout)panel2.getLayout()).setHGap(5);
  			((TableLayout)panel2.getLayout()).setVGap(5);

  			//======== panel3 ========
  			{
  				panel3.setOpaque(false);
  				panel3.setLayout(new GridLayout(1, 2));

  				//---- checkboxRecvStatus ----
  				checkboxRecvStatus.setText("Enable \"Recieve\"");
  				checkboxRecvStatus.setOpaque(false);
  				checkboxRecvStatus.setToolTipText("Task will fire \"send\" status updates");
  				checkboxRecvStatus.setSelected(true);
  				panel3.add(checkboxRecvStatus);

  				//---- checkboxSendStatus ----
  				checkboxSendStatus.setText("Enable \"Send\"");
  				checkboxSendStatus.setOpaque(false);
  				checkboxSendStatus.setToolTipText("Task will fire \"recieve\" status updates");
  				panel3.add(checkboxSendStatus);
  			}
  			panel2.add(panel3, new TableLayoutConstraints(0, 0, 0, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- ttfProgressMsg ----
  			ttfProgressMsg.setText("Loading map from Google Static Maps");
  			ttfProgressMsg.setToolTipText("Set the task progress message here");
  			panel2.add(ttfProgressMsg, new TableLayoutConstraints(2, 0, 2, 0, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- progressBar ----
  			progressBar.setStringPainted(true);
  			progressBar.setString("progress %");
  			progressBar.setToolTipText("% progress is displayed here");
  			panel2.add(progressBar, new TableLayoutConstraints(0, 1, 0, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));

  			//---- lblProgressStatus ----
  			lblProgressStatus.setText("task status listener");
  			lblProgressStatus.setHorizontalTextPosition(SwingConstants.LEFT);
  			lblProgressStatus.setHorizontalAlignment(SwingConstants.LEFT);
  			lblProgressStatus.setToolTipText("Task status messages are displayed here when the task runs");
  			panel2.add(lblProgressStatus, new TableLayoutConstraints(2, 1, 2, 1, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));
  		}
  		contentPanel.add(panel2, new TableLayoutConstraints(0, 2, 0, 2, TableLayoutConstraints.FULL, TableLayoutConstraints.FULL));
  	}
  	//customized gui changes to fit map and map controls accordingly
  			contentPanel.setMinimumSize(new Dimension(550, 550));
			contentPanel.setMaximumSize(new Dimension(550, 550));
			contentPanel.setPreferredSize(new Dimension(550, 550));
			dialogPane.add(contentPanel, BorderLayout.WEST);

			dialogPane.add(mapContent, BorderLayout.CENTER);
			dialogPane.add(mapOptions, BorderLayout.EAST);
  }
 
		dialogScroll.setViewportView(dialogPane);
		contentPane.add(dialogScroll, BorderLayout.CENTER);
		
		//custom size such that every gui element is in place correctly
		setSize(1508, 621);
		setLocationRelativeTo(null);
		
		
		// JFormDesigner - End of component initialization
		// //GEN-END:initComponents
}
	class PanningActionBtn implements ActionListener {

		@Override
		public void actionPerformed(ActionEvent e) {
			/*a bunch of us figured this out together but special 
			thanks to Husain Fazal for the formula!*/
			int zoom = Integer.parseInt(ttfZoom.getText());
			double toadd = 131.072 / java.lang.Math.pow(2, zoom + 1);

			if (e.getSource() == btnup) {
				if ((Double.parseDouble(ttfLat.getText()) + toadd) > 85) {
					ttfLat.setText(Double.toString(Double.parseDouble(ttfLat
							.getText()) + toadd - 170));
				} else {
					ttfLat.setText(Double.toString(Double.parseDouble(ttfLat
							.getText()) + toadd));
				}
			} else if (e.getSource() == btndown) {
				if ((Double.parseDouble(ttfLat.getText()) - toadd) < -85) {
					ttfLat.setText(Double.toString(Double.parseDouble(ttfLat
							.getText()) - toadd + 170));	
				} else {
					ttfLat.setText(Double.toString(Double.parseDouble(ttfLat
							.getText()) - toadd));
				}
			} else if (e.getSource() == btnleft) {
				if ((Double.parseDouble(ttfLon.getText()) - toadd) < -180) {
					ttfLon.setText(Double.toString(Double.parseDouble(ttfLon
							.getText()) - toadd + 360));
				} else {
					ttfLon.setText(Double.toString(Double.parseDouble(ttfLon
							.getText()) - toadd));	
				}
			} else if (e.getSource() == btnright) {
				if ((Double.parseDouble(ttfLon.getText()) + toadd) > 180) {
					ttfLon.setText(Double.toString(Double.parseDouble(ttfLon
							.getText()) + toadd - 360));
				} else {
					ttfLon.setText(Double.toString(Double.parseDouble(ttfLon
							.getText()) + toadd));
				}
			}
			startTaskAction();
		}

	}
// JFormDesigner - Variables declaration - DO NOT MODIFY  //GEN-BEGIN:variables
// Generated using JFormDesigner non-commercial license
	
private JPanel dialogPane;
private JPanel contentPanel;

static final int ZOOM_MIN = 0;
	static final int ZOOM_MAX = 19;
	static final int ZOOM_INIT = 14;
	private JSlider zoomSlider;
	private JPanel zoomPanel;

	private JScrollPane dialogScroll;
	
	private JPanel panning;
	private JButton btnup;
	private JButton btndown;
	private JButton btnleft;
	private JButton btnright;
	
	private JPanel mapContent;
	private JPanel mPanel1;
	private JPanel mapOptions;
	
private JPanel panel1;
private JLabel label2;
private JTextField ttfSizeW;
private JLabel label4;
private JTextField ttfLat;
private JButton btnGetMap;
private JLabel label3;
private JTextField ttfSizeH;
private JLabel label5;
private JTextField ttfLon;
private JButton btnQuit;
private JLabel label1;

private JLabel label6;
private JTextField ttfZoom;
private JScrollPane scrollPane1;
private JTextArea ttaStatus;
private JPanel panel2;
private JPanel panel3;
private JCheckBox checkboxRecvStatus;
private JCheckBox checkboxSendStatus;
private JTextField ttfProgressMsg;
private JProgressBar progressBar;
private JLabel lblProgressStatus;

private JComboBox jcmaptype;
private JPanel maptypepanel;
private String maptype;
// JFormDesigner - End of variables declaration  //GEN-END:variables
}
